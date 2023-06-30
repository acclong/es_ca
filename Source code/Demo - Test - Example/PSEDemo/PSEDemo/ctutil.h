/*
 * $Id: prod/include/ctutil.h 1.1.2.2 2010/04/01 14:02:44EDT Belem,Karim (kbelem) Exp  $
 * $Author: Belem,Karim (kbelem) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/ctutil.h $
 * $Revision: 1.1.2.2 $
 * $Date: 2010/04/01 14:02:44EDT $
 */

/*
    CRYPTOKI additional services API
    Note that functions in here are not a part of the CRYPTOKI standard
    but are useful for applications using CRYPTOKI.
*/

#ifndef CTUTIL_INCLUDED
#define CTUTIL_INCLUDED

#include <stddef.h>
#include <time.h>

#ifdef __cplusplus
extern "C" {        /* define as 'C' functions to prevent mangling */
#endif

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

#ifdef V1COMPLIANT
#define CheckCryptokiVersion() CT_CheckCryptokiVersion(1)
#else
#define CheckCryptokiVersion() CT_CheckCryptokiVersion(2)
#endif
CK_RV CT_CheckCryptokiVersion(int Ver);

/* Only applicable to building the Firmware */
#ifdef __arm
char *strdup(const char *s1);
#endif

CK_RV FindTokenFromName( char * label, CK_SLOT_ID * pslotID );
CK_RV FindKeyFromName(const char * sender, CK_OBJECT_CLASS cl,
    CK_SLOT_ID * phSlot, CK_SESSION_HANDLE * phSession,
    CK_OBJECT_HANDLE * phKey);
CK_RV CT_CreateObject(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_CLASS cl,
    char * name,
    CK_OBJECT_HANDLE * phObj
    );
CK_RV CT_CreatePublicObject(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_CLASS cl,
    char * name,
    CK_OBJECT_HANDLE * phObj
    );
CK_RV CT_OpenObject(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_CLASS cl,
    char * name,
    CK_OBJECT_HANDLE * phObj
    );
CK_RV CT_RenameObject(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_CLASS cl,
    char * oldName,
    char * newName
    );
CK_RV CT_ReadObject(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE hObj,
    unsigned char * buf,
    CK_SIZE len,
    CK_SIZE * pbr
    );
CK_RV CT_WriteObject(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE hObj,
    const unsigned char * buf,
    CK_SIZE len,
    CK_SIZE * pbr
    );

/**
 * Get the security mode for the slot id given by inputSlotID.
 *
 * @param inputSlotId
 *  The slot ID to retrieve the security flags from.
 *
 * @param pAdminSlotId
 *  Location to store the id of the Admin Slot. Optional - Ignored if NULL.
 *
 * @param pSecMode
 *  Location to store the security mode.
 */
CK_RV GetSecurityMode(CK_SLOT_ID inputSlotId, 
                      CK_SLOT_ID* pAdminSlotId,
                      CK_FLAGS* pSecMode);

/*
**  Attribute wrapper functions.
**  GetAttr(), SetAttr().
**  These functions hide some of the mess of grabbing an attribute.
**  They allow attributes to be picked off an object in a single call
**  rather than first setting up structures to make the call.
*/
CK_RV
GetAttr(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE obj,
    CK_ATTRIBUTE_TYPE type,
    CK_VOID_PTR buf, CK_SIZE len, CK_SIZE_PTR size
);
CK_RV
SetAttr(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE obj,
    CK_ATTRIBUTE_TYPE type,
    const CK_VOID_PTR buf, CK_SIZE len
);

/*
**  Find an attribute in an attribute template.
*/
CK_ATTRIBUTE *
FindAttribute(
        CK_ATTRIBUTE_TYPE attrType,
        const CK_ATTRIBUTE * attr,
        CK_COUNT attrCount
        );

/*
**  Calculate and return an AS2805 kvc for a key.
**  The key must be capable of doing an encryption
**  operation using the supplied mechanism for this
**  to succeed. Note that mechanism parameters will
**  be set to NULL.
*/
CK_RV calcKvcMech(CK_SESSION_HANDLE hSession, CK_OBJECT_HANDLE hKey,
        CK_MECHANISM_TYPE mt,
        unsigned char * kvc, CK_SIZE kvclen, CK_SIZE * pkvclen);
/* This one works out the mechanism to use from the key type */
CK_RV calcKvc(CK_SESSION_HANDLE hSession, CK_OBJECT_HANDLE hKey,
        unsigned char * kvc, CK_SIZE kvclen, CK_SIZE * pkvclen);
/*
**  Convert a Cryptoki error code into a printable string.
*/
CK_RV CT_ErrorString(CK_RV ret, char * errstr, size_t len);
/*
**  C_ErrorString has been depricated and replaced with CT_ErrorString.
**  This definition is provided for backwards compatibility only.
*/
#define C_ErrorString CT_ErrorString

/* convert CK?_... definitions to strings */
/* convert strings to definitions CK?_... */

/* associate a numeric value with a printable string */
struct StrMap {
    CK_NUMERIC val; char * text;
};
typedef struct StrMap StrMap;

extern StrMap ObjClassTable[];
extern StrMap KeyTypeTable[];
extern StrMap AttributeTable[];
extern StrMap MechanismTable[];
extern StrMap ErrorTable[];

/**
 * Get the value of the given attribute as a printable string
 *
 * @param pAttr
 *  Pointer to the attribute whose value will be stringified
 *
 * @param pStringVal
 *  Location to hold the value as a string. If this is NULL, the length
 *  required to hold the string will still be copied into pStringValLen
 *
 * @param pStringValLen
 *  Location to store the length of the value as a string. If pStringVal
 *  was supplied, this will contain the number of bytes copied into the 
 *  buffer. If pStringVal is NULL, this will contain the required size of
 *  the buffer to hold the value as a string.
 */
CK_RV CT_AttrToString(CK_ATTRIBUTE_PTR pAttr, 
                      char* pStringVal,
                      CK_SIZE* pStringValLen);

char * strObjClass( CK_NUMERIC val );
CK_NUMERIC valObjClass( const char * txt );
char * strKeyType( CK_NUMERIC val );
CK_NUMERIC valKeyType( const char * txt );
char * strAttribute( CK_NUMERIC val );
CK_NUMERIC valAttribute( const char * txt );
char * strMechanism( CK_NUMERIC val );
CK_NUMERIC valMechanism( const char * txt );
char * strSesState( CK_NUMERIC val );
CK_NUMERIC valSesState( const char * txt );
char * strError( CK_NUMERIC val );
CK_NUMERIC valError( const char * txt );

/*
**  Create keys.
*/
CK_RV CreateSecretKey(CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv,
    CK_KEY_TYPE kt,
    CK_BYTE * keyValue, CK_SIZE len,
    CK_OBJECT_HANDLE * phKey);
CK_RV CreateDesKey(CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv,
    CK_BYTE * keyValue, CK_SIZE len,
    CK_OBJECT_HANDLE * phKey);

CK_RV BuildRsaCrtKeyPair(
    CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv,
    CK_OBJECT_HANDLE * phPub, CK_OBJECT_HANDLE * phPri,
    char * modulusStr, char * pubExpStr,
    char * priExpStr, char * priPStr, char * priQStr,
    char * priE1Str, char * priE2Str, char * priUStr
    );
CK_RV BuildRsaKeyPair(CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv,
    CK_OBJECT_HANDLE * phPub, CK_OBJECT_HANDLE * phPri,
    char * modulusStr, char * pubExponentStr, char * priExponentStr);
CK_RV GenerateRsaKeyPair(CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv,
    CK_SIZE modulusBits, int expType,
    CK_OBJECT_HANDLE * phPublicKey,
    CK_OBJECT_HANDLE * phPrivateKey);

CK_RV BuildDsaKeyPair(CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv,
    CK_OBJECT_HANDLE * phPub, CK_OBJECT_HANDLE * phPri,
    char * prime, char * subprime, char * base, char * pub, char * pri);
CK_RV GenerateDsaKeyPair(CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv, int param,
    CK_SIZE valueBits,
    CK_OBJECT_HANDLE * phPublicKey,
    CK_OBJECT_HANDLE * phPrivateKey);

CK_RV BuildDhKeyPair(CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv,
    CK_OBJECT_HANDLE * phPub, CK_OBJECT_HANDLE * phPri,
    char * prime, char * base, char * pub, char * pri);
CK_RV GenerateDhKeyPair(CK_SESSION_HANDLE hSession, char * txt,
    int tok, int priv, int param,
    CK_SIZE valueBits,
    CK_OBJECT_HANDLE * phPublicKey,
    CK_OBJECT_HANDLE * phPrivateKey);

CK_RV
BuildEcKeyPair(CK_SESSION_HANDLE hSession, char * txt,
	int tok, int priv,
	CK_OBJECT_HANDLE * phPub, CK_OBJECT_HANDLE * phPri,
	char * params, char * value, char * subject, char * point, char * id);

CK_RV
TransferObject(
    CK_SESSION_HANDLE sTo,
    CK_SESSION_HANDLE sFrom,
    CK_OBJECT_HANDLE hObj,
    CK_OBJECT_HANDLE * phObj,
    CK_ATTRIBUTE_PTR pTemplate,
    CK_COUNT ulCount );

void DumpAttributes(CK_ATTRIBUTE * na, CK_COUNT attrCount);

/* 
 * Determine the total number of sessions open on all 
 * tokens on all adapters.
 */
CK_RV GetTotalSessionCount(CK_COUNT *pSessionCount);

/*
**  Debug assistance functions
*/
CK_RV GetObjectCount(CK_SLOT_ID slotID, CK_COUNT * pCount);
CK_RV GetSessionCount(CK_SLOT_ID slotID,
        CK_COUNT * pSessionCount, CK_COUNT *prwSessionCount);
CK_RV DumpRSAKeyPair(int cStyle, CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE hPub, CK_OBJECT_HANDLE hPri);
CK_RV DumpDSAKeyPair(int cStyle, CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE hPub, CK_OBJECT_HANDLE hPri);
CK_RV DumpDHKeyPair(int cStyle, CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE hPub, CK_OBJECT_HANDLE hPri);
CK_RV GetDeviceError(CK_SLOT_ID slotID, CK_NUMERIC *pDeviceError);

int cDump(char * title, unsigned char * buf, size_t len);
void rmTrailSpace(char * txt, int len);

void DateConvert(char * fmt, const char * ts);
void DateConvertGmtToLocal(char * fmt, const char * ts);
CK_RV ShowSlot(CK_SLOT_ID slotID, int verbose);
CK_RV ShowToken(CK_SLOT_ID slotID, int verbose);

/* Helper function to provide the DER encoding of a supported named curve. */
CK_RV CT_DerEncodeNamedCurve(CK_BYTE_PTR buf,
                             CK_SIZE_PTR len,
                             const char *name);

/* Helper function to return key size (in bits) for a given EC parameter */
CK_RV CT_GetECKeySize(const CK_ATTRIBUTE_PTR ecParam,
                      CK_SIZE_PTR            size);

/* Helper function to read ECC domain parameters from a supported curve or from a Domain Parameter object */
CK_RV CT_GetECCDomainParameters(CK_SESSION_HANDLE hSession,
                                CK_ATTRIBUTE_PTR  attr,
                             const char *name);

/* Gets the random challenge */
CK_RV CT_GetAuthChallenge(CK_SESSION_HANDLE hSession,
        CK_BYTE_PTR pChallenge,
        CK_ULONG_PTR pulChallengeLen);

/* Gets the temporary pin */
CK_RV CT_GetTmpPin(CK_SESSION_HANDLE hSession,
        CK_BYTE_PTR pPin,
        CK_ULONG_PTR pulPinLen);

/* 
 * These functions perform Base64 encoding and Base64 decoding.
 * Base64 encoding is often also called PEM encoding.
 *
 * Armoring is the term used in PGP and MIME to describe the formatting of binary data such 
 * that it can be unambiguously embedded in a printable document such as an email.
 */

CK_RV CT_Structure_To_Armor( 
    
    char *          pLabel,		/* IN Armor label (string) */
    char * 	      pComment,		/* IN optional comment string (my be NULL) */
    CK_VOID_PTR 	 pData, 	/* IN data to armor */
    CK_ULONG 	 ulDataLen, 	/* IN length of data */

    CK_BYTE_PTR *	ppArmor, 	/* OUT Armored structure created */
    CK_ULONG_PTR pulArmorLen	/* IN/OUT pArmor buffer length */
);

CK_RV CT_Structure_From_Armor( 
    
    char *	     pLabel,		/* IN Armor label (string) */
    CK_BYTE_PTR 	pArmor,		/* IN Armored structure */
    CK_ULONG 	ulArmorLen,	    /* IN pArmor buffer length */
    
    CK_VOID_PTR *	pData, 		/* OUT extracted structure */
    CK_ULONG_PTR 	pulDataLen 	/* IN/OUT pData buffer length */
);

/*
 * compute the object digest as used by SET Attributes Ticket to identify
 * the target object.
 */

CK_RV CT_GetObjectDigest(
                 CK_SESSION_HANDLE hSession,  /* IN */
                 CK_OBJECT_HANDLE hObject,    /* IN */
                 CK_MECHANISM_PTR pDigestMech,/* IN */

                 CK_BYTE_PTR *    ppDigest,   /* OUT returned buffer must be freed */
                 CK_ULONG    *    pulDigest   /* OUT length of returned buffer */
                 );

/*
 * convert time_t structure to the DATE format used by CT_SetLimitsAttributes
 * and CT_Create_Set_Attributes_Ticket_Info
 */

void  CT_SetCKDateStrFromTime(char pd[9], /* OUT - pointer to a bugger at least 9 bytes long */
                              time_t *t);  /* IN - time value to convert */


/*
 * Apply limit attributes to an object.
 * The optional inputs maybe set to NULL to indicte that that Attributes should not be set.
 */

CK_RV CT_SetLimitsAttributes( CK_SESSION_HANDLE hSession, /* IN */
                              CK_OBJECT_HANDLE  hObj,     /* IN */
                              CK_VOID_PTR 	pCertData,    /* IN - optional CKA_ADMIN_CERT value */
                              CK_ULONG  	ulCertDataLen,/* IN - length of pCertData */
                              CK_ULONG    * usage_limit,  /* IN - optional CKA_USAGE_LIMIT */
                              CK_ULONG    * usage_count,  /* IN - optional CKA_USAGE_COUNT */
                              char *        start_date,   /* IN - optional CKA_START_DATE */
                              char *        end_date      /* IN - optional CKA_END_DATE */
                            );

/* 
 * change an object CKA_MODIFIABLE attribute from true to False.
 * This involves copying the object - so the handle of the object will change.
 * The original object is deleted.
 */

CK_RV CT_MakeObjectNonModifiable( CK_SESSION_HANDLE hSession,   /* IN */
                                  CK_OBJECT_HANDLE  hObj,       /* IN */
                                  CK_OBJECT_HANDLE *phObj       /* OUT (may be NULL) */
                                );
/*
 * compute the object digest as used by SET Attributes Ticket to identify
 * the target object by using parts
 */

CK_RV CT_GetObjectDigestFromParts(
                 CK_SESSION_HANDLE hSession,  /* IN */
                 CK_MECHANISM_PTR pDigestMech,/* IN */
                 char * tokenSerialNumber,    /* IN */
                 char * tokenLabel,           /* IN */
                 char * objLabel,             /* IN */
                 CK_BYTE_PTR  objID,          /* IN */
                 CK_ULONG     objIDlen,       /* IN */

                 CK_BYTE_PTR *    ppDigest,   /* OUT returned buffer (must be freed by caller) */
                 CK_ULONG    *    pulDigest   /* OUT length of returned buffer */
                 );
                           
/* construct the data to be signed in an Attribute Certificate as used for
 * set-attributes ticket.
 */

CK_RV CT_Create_Set_Attributes_Ticket_Info
( 
    CK_MECHANISM_TYPE objectDigestAlg, /* digest alg */
    unsigned char * objectDigest, /* digest of target object */
	unsigned int objectDigestLen,
    char * issuerRDN,       /* may be NULL or 
                             * DER of DistName or 
                             * Common Name string or
                             * RDN Seq string (CN=Fred+C=USA) */
    unsigned int issuerRDNLen,
    CK_MECHANISM_TYPE signatureAlg, /* signature alg used later to sign the info */
    unsigned long sno,      /* Attrib Cert serial number */
    char * notBefore,       /* YYYYMMDD string */
    char * notAfter,        /* YYYYMMDD string */


    unsigned long * limit,  /* NULL if no CKA_USAGE_LIMIT */
    char * start,           /* NULL if no CKA_START_DATE */
    char * end,             /* NULL if no CKA_END_DATE */
    char * cert,            /* NULL if no CKA_ADMIN_CERT */
    unsigned int certLen,

    CK_CHAR_PTR * ppTicketInfo, /* OUT new unsigned ticket returned here - must be freed by caller */
    unsigned int* puiTicketLen  /* OUT *pTicketInfo buffer length */
);

CK_RV CT_Create_Set_Attributes_Ticket( 
            void * pTicketInfo,    		    /* IN unsigned ticket info */
            unsigned int uiTicketInfoLen, 	/* IN pTicketInfo buffer length */

            CK_MECHANISM_TYPE signatureAlg, /* signature alg */
            unsigned char * pSignature, 	/* IN signature of pTicketData */
            unsigned int uiSigLen, 		    /* IN pSignature buffer length */

            CK_BYTE_PTR * ppTicketData,     /* OUT new signed ticket (caller must free) */
            unsigned int * puiTicketLen 	/* IN/OUT pTicketData buffer length */
                                    );


#ifdef __cplusplus
}
#endif

#endif
