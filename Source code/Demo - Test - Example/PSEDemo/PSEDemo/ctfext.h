#ifndef CTFEXT_INCLUDED
#define CTFEXT_INCLUDED

#ifdef __cplusplus
extern "C" {
#endif

/*  This function is an ERACOM extension to the PKCS#11.

	This function allows adapter administrator to initialize a token.

	Return Value:
	CKR_OK: Operation successful.
	CKR_ARGUMENTS_BAD: pLabel is NULL, or pPin is NULL, although pinLen is nonzero.
	CKR_SESSION_HANDLE_INVALID: hSession is invalid.
	CKR_USER_NOT_LOGGED_IN: user is not logged in to the admin token.
	CKR_NOT_ADMIN_TOKEN: hSession is opened to the administration token.
	CKR_TOKEN_NOT_PRESENT: The hSession does not have a token (this is an
	internal consistency error).
	CKR_SLOT_ID_INVALID: The slot ID is not a valid token on the system, or it
	is already initialized.
*/
CK_RV CK_ENTRY CT_InitToken(
		CK_SESSION_HANDLE hSession,
		CK_SLOT_ID slotId,
		CK_CHAR_PTR pPin,
		CK_ULONG ulPinLen,
		CK_CHAR_PTR pLabel
);

#if defined (INSAM) || defined (OS2) || defined(WIN32)
typedef CK_RV (CK_ENTRY *CK_CT_InitToken)(
		CK_SESSION_HANDLE hSession,
		CK_SLOT_ID slotId,
		CK_CHAR_PTR pPin,
		CK_ULONG ulPinLen,
		CK_CHAR_PTR pLabel
);
#else
typedef CK_RV CK_ENTRY (*CK_CT_InitToken)(
		CK_SESSION_HANDLE hSession,
		CK_SLOT_ID slotId,
		CK_CHAR_PTR pPin,
		CK_ULONG ulPinLen,
		CK_CHAR_PTR pLabel
);
#endif


/*  This function is an ERACOM extension to PKCS #11.

	It will erase (reset) the token which the token is connected to. The session
	must be in RW SO mode for this function to succeed.

	Return Values:
	CKR_OK: Operation successful.
	CKR_ARGUMENTS_BAD: pLabel is NULL, or pPin is NULL, although pinLen is nonzero.
	CKR_SESSION_HANDLE_INVALID: hSession is invalid.
	CKR_USER_NOT_LOGGED_IN: The SO is not logged in to the session.
	CKR_TOKEN_NOT_PRESENT: The token is not in the slot.
	CKR_SESSION_EXISTS: Other sessions are active to the same token.
*/
CK_RV CK_ENTRY CT_ResetToken(
		CK_SESSION_HANDLE hSession,
		CK_CHAR_PTR pPin,
		CK_ULONG ulPinLen,
		CK_CHAR_PTR pLabel
);

#if defined (INSAM) || defined (OS2) || defined(WIN32)
typedef CK_RV (CK_ENTRY *CK_CT_ResetToken)(
		CK_SESSION_HANDLE hSession,
		CK_CHAR_PTR pPin,
		CK_ULONG ulPinLen,
		CK_CHAR_PTR pLabel
);
#else
typedef CK_RV CK_ENTRY (*CK_CT_ResetToken)(
		CK_SESSION_HANDLE hSession,
		CK_CHAR_PTR pPin,
		CK_ULONG ulPinLen,
		CK_CHAR_PTR pLabel
);
#endif

/*  This function is an ERACOM extension to PKCS #11.

	It will copy an object from a token to another token.

	Return Values:
	CKR_OK: Object copied successfully.
	CKR_SESSION_HANDLE_INVALID: Either source or destination session handle is
	invalid.
	CKR_OBJECT_HANDLE_INVALID: The source object handle is invalid.
	CKR_TOKEN_NOT_PRESENT: Either the source token, or the destination token
	does not exist.
	CKR_ARGUMENTS_BAD: phNewObject is NULL, or the attribute template is invalid.
	CKR_HOST_MEMORY: Not enough memory on the host system to carry out request.
	CKR_DEVICE_MEMORY: Not enough memory on the device to complete request.
*/
CK_RV CK_ENTRY CT_CopyObject(
		CK_SESSION_HANDLE hSesDest,
		CK_SESSION_HANDLE hSesSrc,
		CK_OBJECT_HANDLE hObject,
		CK_ATTRIBUTE_PTR pTemplate,
		CK_COUNT ulCount,
		CK_OBJECT_HANDLE_PTR phNewObject
);

#if defined (INSAM) || defined (OS2) || defined(WIN32)
typedef CK_RV (CK_ENTRY *CK_CT_CopyObject)(
		CK_SESSION_HANDLE hSesDest,
		CK_SESSION_HANDLE hSesSrc,
		CK_OBJECT_HANDLE hObject,
		CK_ATTRIBUTE_PTR pTemplate,
		CK_COUNT ulCount,
		CK_OBJECT_HANDLE_PTR phNewObject
);
#else
typedef CK_RV CK_ENTRY (*CK_CT_CopyObject)(
		CK_SESSION_HANDLE hSesDest,
		CK_SESSION_HANDLE hSesSrc,
		CK_OBJECT_HANDLE hObject,
		CK_ATTRIBUTE_PTR pTemplate,
		CK_COUNT ulCount,
		CK_OBJECT_HANDLE_PTR phNewObject
);
#endif

/*  This function is an ERACOM extension to PKCS #11.

	It returns the real HSM id for the specified user Slot ID.

    Return Values:
	CKR_OK: Successfully mapped the slotID to a physical HSM.
    CKR_ARGUMENTS_BAD: The supplied pHsmID is NULL.
    CKR_SLOT_ID_INVALID: The supplied slotID does not exist.
    CKR_FUNCTION_NOT_SUPPORTED: The library is in WLD mode, and this
                                functionality is not supported.
*/
CK_RV CK_ENTRY CT_HsmIdFromSlotId(CK_SLOT_ID slotID, unsigned int *pHsmID);

#if defined (INSAM) || defined (OS2) || defined(WIN32)
typedef CK_RV (CK_ENTRY *CK_CT_HsmIdFromSlotId)(
        CK_SLOT_ID slotID,
        unsigned int *pHsmID
);
#else
typedef CK_RV CK_ENTRY (*CK_CT_HsmIdFromSlotId)(
        CK_SLOT_ID slotID,
        unsigned int *pHsmID
);
#endif

/*  This function is an ERACOM extension to PKCS #11.

	It return the HSM session handle for the specified user Session Handle.

    Return Values:
	CKR_OK: Successfully mapped the user Session Handle to a HSM Session Handle.
    CKR_ARGUMENTS_BAD: The supplied pHsmID is NULL.
    CKR_FUNCTION_NOT_SUPPORTED: The library is in WLD mode, and this
                                functionality is not supported.
*/
CK_RV CK_ENTRY CT_ToHsmSession(
                    CK_SESSION_HANDLE hSession,
                    CK_SESSION_HANDLE_PTR phHsmSession
);

#if defined (INSAM) || defined (OS2) || defined(WIN32)
typedef CK_RV (CK_ENTRY *CK_CT_ToHsmSession)(
        CK_SESSION_HANDLE hSession,
        CK_SESSION_HANDLE_PTR phHsmSession
);
#else
typedef CK_RV CK_ENTRY (*CK_CT_ToHsmSession)(
        CK_SESSION_HANDLE hSession,
        CK_SESSION_HANDLE_PTR phHsmSession
);
#endif

/*  This function is an ERACOM extension to PKCS #11.

	It Presents a authorised Ticket to the session

    Return Values:
*/
CK_RV CK_ENTRY CT_PresentTicket(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE hObj,
    CK_MECHANISM_PTR pMechanism,
    CK_BYTE_PTR pTicket, 
    CK_ULONG ulTicketLen 
);

#if defined (INSAM) || defined (OS2) || defined(WIN32)
typedef CK_RV (CK_ENTRY *CK_CT_PresentTicket)(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE hObj,
    CK_MECHANISM_PTR pMechanism,
    CK_BYTE_PTR pTicket, 
    CK_ULONG ulTicketLen 
);
#else
typedef CK_RV CK_ENTRY (*CK_CT_PresentTicket)(
    CK_SESSION_HANDLE hSession,
    CK_OBJECT_HANDLE hObj,
    CK_MECHANISM_PTR pMechanism,
    CK_BYTE_PTR pTicket, 
    CK_ULONG ulTicketLen 
);
#endif
/*  This function is an ERACOM extension to PKCS #11.

	It return the HSM session handle for the specified user Session Handle.

    Return Values:
	CKR_OK: Successfully mapped the user Session Handle to a HSM Session Handle.
    CKR_ARGUMENTS_BAD: The supplied pHsmID is NULL.
    CKR_FUNCTION_NOT_SUPPORTED: The library is in WLD mode, and this
                                functionality is not supported.
*/
CK_RV CK_ENTRY CT_SetHsmDead(
                    CK_ULONG hsmIDx, 
                    CK_BBOOL bDisable
);

#if defined (INSAM) || defined (OS2) || defined(WIN32)
typedef CK_RV (CK_ENTRY *CK_CT_SetHsmDead)(
        CK_ULONG hsmIDx, 
        CK_BBOOL bDisable
);
#else
typedef CK_RV CK_ENTRY (*CK_CT_SetHsmDead)(
        CK_ULONG hsmIDx, 
        CK_BBOOL bDisable
);
#endif

/*  This function is an SafeNet extension to PKCS #11.
	It return the HSM session handle for the specified user Session Handle.
*/
CK_RV CK_ENTRY CT_GetHSMId(
					CK_SESSION_HANDLE hSession,
					CK_ULONG_PTR hsmid
);

#if defined (INSAM) || defined (OS2) || defined(WIN32)
typedef CK_RV (CK_ENTRY *CK_CT_GetHSMId)(
        CK_SESSION_HANDLE hSession,
		CK_ULONG_PTR hsmid
);
#else
typedef CK_RV CK_ENTRY (*CK_CT_GetHSMId)(
        CK_SESSION_HANDLE hSession,
		CK_ULONG_PTR hsmid
);
#endif


#ifdef __cplusplus
}
#endif

#endif
