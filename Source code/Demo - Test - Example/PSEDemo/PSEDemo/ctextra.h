/*
 * $Id: prod/include/ctextra.h 1.1.2.1 2010/01/29 14:37:34EST Belem,Karim (kbelem) Exp  $
 * $Author: Belem,Karim (kbelem) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/ctextra.h $
 * $Revision: 1.1.2.1 $
 * $Date: 2010/01/29 14:37:34EST $
 */

/*
	CRYPTOKI additional services API
	Note that functions in here are not a part of the CRYPTOKI standard
	but are useful for applications using CRYPTOKI.
*/

#ifndef CTEXTRA_INCLUDED
#define CTEXTRA_INCLUDED

#ifdef __cplusplus
extern "C" {			/* define as 'C' functions to prevent mangling */
#endif

/*
 * Global Variables
 */
extern int VdefConversions; /* encattr.c and hsmencoders.c */

/* This macro is used to obtain the number of items in an array. The parameter
   passed to it must have been declared as an array either in the function
   local, or module global variables area. It will not work with function
   parameters which are arrays.
   WARNING: The parameter is evaluated twice. So it should not be used in a
   complex statement. E.g.: ARRAY_SIZE(x++) will increment x twice.
*/
#ifndef NUMITEMS
#define NUMITEMS(X)  (sizeof(X)/sizeof((X)[0]))
#endif

/*
**	Mechanism list
*/
struct TOK_MECH_DATA {
	CK_MECHANISM_TYPE * pMechanisms;
	unsigned int count;
};
typedef struct TOK_MECH_DATA TOK_MECH_DATA;

int LookupMech(TOK_MECH_DATA * pMech, CK_MECHANISM_TYPE mechType);
void FreeMechData(TOK_MECH_DATA * pMech);

/* mechanism support functions */
CK_MECHANISM_TYPE * genkMechanismTabFromMechanismTab(TOK_MECH_DATA * mTab,
						CK_COUNT * len);
CK_MECHANISM_TYPE * genkpMechanismTabFromMechanismTab(TOK_MECH_DATA * mTab,
						CK_COUNT * len);
CK_MECHANISM_TYPE * mechFromKt(CK_KEY_TYPE kt, CK_COUNT * len);
CK_KEY_TYPE * ktFromMech( CK_MECHANISM_TYPE mt, CK_COUNT * len);
CK_MECHANISM_TYPE * mechFromTokKt(TOK_MECH_DATA * mTab, CK_KEY_TYPE kt,
						CK_COUNT * len);
CK_MECHANISM_TYPE * mechDeriveFromKt(CK_KEY_TYPE kt, CK_COUNT * len);
CK_MECHANISM_TYPE * mechSignFromKt(CK_KEY_TYPE kt, CK_COUNT * len);
CK_MECHANISM_TYPE * mechSignRecFromKt(CK_KEY_TYPE kt, CK_COUNT * len);
CK_MECHANISM_TYPE * hashMech(CK_COUNT * len);
CK_MECHANISM_TYPE * kgMech(CK_COUNT * len);
CK_MECHANISM_TYPE * kpgMech(CK_COUNT * len);

int isGenMech(CK_MECHANISM_TYPE mechType);

int isPbeMech(CK_MECHANISM_TYPE mechType);
int isRc2Mech(CK_MECHANISM_TYPE mechType);

/* object attribute list managed as an array */
struct TOK_ATTR_DATA {
	CK_ATTRIBUTE * attributes;  /* an array of attribute items */
	CK_COUNT attrCount;	/* number of items in 'attributes' */
};
typedef struct TOK_ATTR_DATA TOK_ATTR_DATA;

/*
**	Extract a numeric attribute from an attribute template.
*/
CK_NUMERIC numAttr(const CK_ATTRIBUTE * at);
CK_NUMERIC numAttrLookup(CK_ATTRIBUTE_TYPE atype, const CK_ATTRIBUTE * attr,
		CK_COUNT attrCount);
unsigned int intAttr(const CK_ATTRIBUTE * at);
unsigned int intAttrLookup(CK_ATTRIBUTE_TYPE atype, const CK_ATTRIBUTE * attr,
		CK_COUNT attrCount);

/*
**	Find an attribute in an attribute set.
*/
CK_ATTRIBUTE *
FindAttr(
		CK_ATTRIBUTE_TYPE attrType,
		const TOK_ATTR_DATA * attrData
		);

/*
**  Extract the object class and key type from an object.
**  This is a particularly common job when working with
**  CRYPTOKI key objects.
**  Return CKR_OK if both attributes were found.
*/
CK_RV GetObjectClassAndKeyType(
	const TOK_ATTR_DATA * attr,
	CK_OBJECT_CLASS * at_class, CK_KEY_TYPE * kt
);

/**
 * Get the list of attributes (type and size) of the specified object.
 *
 * This function relies on the CKA_ENUM_ATTRIBUTES SafeNet extension to 
 * retrieve the list of attributes. Only the attribute type and size are
 * returned, it is up to the caller to retrieve the attribute values if so 
 * desired.
 *
 * @param hSession
 *  handle to a valid session
 *
 * @param hObj
 *  handle to the object to operat on
 *
 * @param ppAttributes
 *  location to receive the attribute array. Upon return *ppAttributes
 *  references an array of CK_ATTRIBUTE. It is up to the caller to
 *  free the memory allocatd at *ppAttributes.
 *
 * @param pAttrCount
 *  location to hold the number of CK_ATTRIBUTE entries. Upon return
 *  *pAttrCount is the number of CK_ATTRIBUTE entries referenced by 
 *  *ppAttributes
 *
 * @return
 *  @li CKR_OK for successful execution.
 *  @li Any other CKR_ error code to indicate error condition.
 */
CK_RV GetObjAttrInfo(CK_SESSION_HANDLE hSession,
                     CK_OBJECT_HANDLE hObj,
                     CK_ATTRIBUTE_PTR* ppAttributes,
                     CK_ULONG_PTR pAttrCount);

/**
 * Using fgets, read up to strSize-1 characters from stdin.
 *
 * Any characters entered beyond strSize-1 upto either '\n' or EOF
 * are consumed.
 *
 * Upon successful return, str is always NULL terminated.
 *
 * @param str
 *  location to store resulting string read from stdin.
 *
 * @param strSize
 *  Size of str. At most strSize-1 characters are returned.
 *
 * @return
 *  Upon success, str. NULL for failure.
 */
char* ReadLine(char* str, int strSize);

/*
**	Read an attribute value from an attribute set.
**	Return TRUE if the attribute was present.
*/
int
ReadAttr(
		void * buf, CK_SIZE len, CK_SIZE * plen,
		CK_ATTRIBUTE_TYPE attrType,
		const TOK_ATTR_DATA * attr
		);
/*
**	Add an attribute to an attribute set.
**	Delete attribute if the size is 0.
**	Replace attribute if it is already there, add otherwise.
**	Attribute replacement is done subject to vslidity and
**	consistancy checks.
**
**	Return an error code if the attribute could not be written.
*/
CK_RV
WriteAttr(
		const void * buf, CK_SIZE len,
		CK_ATTRIBUTE_TYPE attrType,
		TOK_ATTR_DATA * attr
		);

/*
**  Make a copy of an attribute set.
**  Return a pointer to the set.
**	Return NULL if list cannot be duplicated.
*/
TOK_ATTR_DATA * DupAttributes(const CK_ATTRIBUTE * attr, CK_COUNT attrCount);
TOK_ATTR_DATA * DupAttributeSet(const TOK_ATTR_DATA * attrData );

/*
**	Copy attributes from one attribute table to another.
**	The target table must have buffers to accomidate all values.
**	Note: No mallocs allowed.
*/
CK_RV at_assign(CK_ATTRIBUTE * tgtNa, const CK_ATTRIBUTE * srcNa);
CK_RV TransferAttr(CK_ATTRIBUTE * pTgtTemplate,
	const CK_ATTRIBUTE * pSrcTemplate, CK_COUNT attrCount);
CK_ATTRIBUTE * CopyAttribute( CK_ATTRIBUTE_TYPE at, TOK_ATTR_DATA * tgt, const TOK_ATTR_DATA * src );

/*
**  Do a comparison of two attribute sets.
**  Every attribute in the 'match' set must be found in the 'ad' set.
**  It is OK if 'ad' is a superset of 'match'.
**  Return TRUE if all attributes in 'match' were found in 'ad'.
*/
int MatchAttributeSet( const TOK_ATTR_DATA * match, const TOK_ATTR_DATA * ad);

/*
**	Add two attribute sets being carefull to drop duplicates
**	The 'base' attributes will override 'user' attributes where
**	duplicates are concerned.
*/
CK_RV AddAttributeSets(TOK_ATTR_DATA ** pSum,
	const TOK_ATTR_DATA * base, const TOK_ATTR_DATA * user);
CK_RV ConcatAttributeSets(TOK_ATTR_DATA * base, const TOK_ATTR_DATA * user);

/*
**  Free an attribute list
*/
TOK_ATTR_DATA * NewAttributeSet(CK_COUNT count);
void FreeAttributeSet(TOK_ATTR_DATA * attr);
void FreeAttributes(CK_ATTRIBUTE_PTR attr, CK_COUNT attrCount);
void FreeAttributesNoClear(CK_ATTRIBUTE_PTR attr, CK_COUNT attrCount);

int isBooleanAttr(const CK_ATTRIBUTE * na);
int isEnumeratedAttr(const CK_ATTRIBUTE * na);
int isNumericAttrType(const CK_ATTRIBUTE_TYPE attr_type);
int isNumericAttr(const CK_ATTRIBUTE * na);
int isSensitiveAttr(const struct TOK_ATTR_DATA * attrData, const CK_ATTRIBUTE * na);

CK_RV ExtractAllAttributes(CK_SESSION_HANDLE hSession, CK_OBJECT_HANDLE hobj,
			TOK_ATTR_DATA ** pna);

/*
**	Password to validation code / Key functions.
*/
void KeyFromPin(unsigned char key[16], unsigned int klen, CK_USER_TYPE user,
	const unsigned char * pin, unsigned int pinLen);
void PvcFromPin(unsigned char * key, unsigned int klen, CK_USER_TYPE user,
	const unsigned char * pin, unsigned int pinLen);

/* wrap a key, encode its attributes and write it to a buffer */
CK_RV WrapEnc( CK_SESSION_HANDLE hSession, CK_OBJECT_HANDLE hWrapper, CK_OBJECT_HANDLE hWrappee, 
	unsigned char * buf, CK_SIZE bufLen, CK_SIZE * bytesWritten );

CK_RV UnwrapDec( CK_SESSION_HANDLE hSession, CK_OBJECT_HANDLE hUnwrapper, CK_OBJECT_HANDLE * hUnwrappedKey,
	unsigned char * buf, CK_SIZE bufLen );

/*
 * Fill in the Cryptoki version information.
 */
CK_VOID GetCryptokiVersion(CK_VERSION_PTR pVer);

#include "endyn.h"

#if defined(V1COMPILANT)
#	define hton_count(x)	hton_short(x)
#	define ntoh_count(x)	ntoh_short(x)
#	define hton_numeric(x)	hton_short(x)
#	define ntoh_numeric(x)	ntoh_short(x)
#else
#	define hton_count(x)	hton_long(x)
#	define ntoh_count(x)	ntoh_long(x)
#	define hton_numeric(x)	hton_long(x)
#	define ntoh_numeric(x)	ntoh_long(x)
#endif

#define hton_flags(x)		hton_long(x)
#define ntoh_flags(x)		ntoh_long(x)
#define hton_slot(x)		hton_long(x)
#define ntoh_slot(x)		ntoh_long(x)
#define hton_handle(x)		hton_long(x)
#define ntoh_handle(x)		ntoh_long(x)

/* Following functions are used to handle PKCS#11 compliant mutex handling. */

/* 
 * This function must be called before any mutex locking is performed.
 * It parses the elements of args, and configures the mutex support module
 * according to the specified arguments.
 *
 * Return Value:
 * CKR_OK: The arguments were parsed, and the mutex support module configured accordingly.
 * CKR_ARGUMENTS_BAD: the contents of args is inconsistent.
 * CKR_CANT_LOCK: An impossible configuration for the platform was requested by
 * the args structure.
 */
CK_RV CT_ParseCInitializeArgs(CK_C_INITIALIZE_ARGS_PTR args);

/* 
 * This function must be called when the cryptoki library is returned to its
 * uninitialized state, so that another C_Initialize() can be called with
 * different arguments.
 *
 * Return Value:
 * CKR_OK: The mutex support system was successfully reset.
 * CKR_CRYPTOKI_NOT_INITIALIZED: A previous successfull
 * CT_ParseCInitializeArgs() call was not performed.
 */
CK_RV CT_ResetCInitializeArgs(void);

/* 
 * This functions is used to create a mutex. If the library is configured not to
 * lock, the function will return CKR_OK, and set the contents of the variable
 * pointed to by ppMutex to a non-NULL value.
 *
 * Return Value:
 * CKR_OK: Successful
 * otherwise: depends on the function supplied in init args.
 */
CK_RV CT_CreateMutex(CK_VOID_PTR_PTR ppMutex);

/* 
 * This function is used to destroy a mutex. If the library is configured not to
 * lock, the function will return CKR_OK without any effect.
 *
 * Return Value:
 * CKR_OK: Successful
 * otherwise: depends on the function supplied in init args.
 */
CK_RV CT_DestroyMutex(CK_VOID_PTR pMutex);
	
/* 
 * This functions is used to lock a mutex. If the library is configured not to
 * lock, the function will return CKR_OK without any effect.
 *
 * Return Value:
 * CKR_OK: Successful
 * otherwise: depends on the function supplied in init args.
 */
CK_RV CT_LockMutex(CK_VOID_PTR pMutex);
	

/* 
 * This functions is used to unlock a mutex. If the library is configured not to
 * lock, the function will return CKR_OK without any effect.
 *
 * Return Value:
 * CKR_OK: Successful
 * otherwise: depends on the function supplied in init args.
 */
CK_RV CT_UnlockMutex(CK_VOID_PTR pMutex);


#ifdef __cplusplus
}
#endif

#endif
