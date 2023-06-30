/*
 * $Id: prod/include/pkcs11f1.h 1.1.2.1 2010/01/29 14:37:26EST Belem,Karim (kbelem) Exp  $
 * $Author: Belem,Karim (kbelem) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/pkcs11f1.h $
 * $Revision: 1.1.2.1 $
 * $Date: 2010/01/29 14:37:26EST $
 */
#ifndef INC_PKCS11F1_H
#define INC_PKCS11F1_H


/* V1COMPLIANT */

CK_RV CK_ENTRY C_GetFunctionList(
	CK_FUNCTION_LIST_PTR_PTR ppFunctionList
);

CK_RV CK_ENTRY C_Initialize(
	CK_VOID_PTR /*CK_C_INITIALIZE_ARGS_PTR*/ pReserved
);

/* part of CRYPTOKI V 2.x definition */
CK_RV CK_ENTRY C_Finalize(
	CK_VOID_PTR pReserved
);

CK_RV CK_ENTRY C_GetInfo(
	CK_INFO_PTR pInfo
);

CK_RV CK_ENTRY C_GetSlotList(
	CK_BBOOL tokenPresent,
	CK_SLOT_ID_PTR pSlotList,
	CK_COUNT_PTR pCount
);

CK_RV CK_ENTRY C_GetSlotInfo(
	CK_SLOT_ID slotID,
	CK_SLOT_INFO_PTR pInfo
);

CK_RV CK_ENTRY C_GetTokenInfo(
	CK_SLOT_ID slotID,
	CK_TOKEN_INFO_PTR pInfo
);

CK_RV CK_ENTRY C_GetMechanismList(
	CK_SLOT_ID slotID,
	CK_MECHANISM_TYPE_PTR pMechanismList,
	CK_COUNT_PTR pCount
);

CK_RV CK_ENTRY C_GetMechanismInfo(
	CK_SLOT_ID slotID,
	CK_MECHANISM_TYPE type,
	CK_MECHANISM_INFO_PTR pInfo
);

CK_RV CK_ENTRY C_InitToken(
	CK_SLOT_ID slotID,
	CK_CHAR_PTR pPin,
	CK_SIZE pinLen,
	CK_CHAR_PTR pLabel
);

CK_RV CK_ENTRY C_InitPIN(
	CK_SESSION_HANDLE hSession,
	CK_CHAR_PTR pPin,
	CK_SIZE pinLen
);
CK_RV CK_ENTRY C_SetPIN(
	CK_SESSION_HANDLE hSession,
	CK_CHAR_PTR pOldPin,
	CK_SIZE oldLen,
	CK_CHAR_PTR pNewPin,
	CK_SIZE newLen
);

CK_RV CK_ENTRY C_OpenSession(
	CK_SLOT_ID slotID,
	CK_FLAGS flags,
	CK_VOID_PTR pApplication,
	CK_NOTIFY Notify,
	CK_SESSION_HANDLE_PTR phSession
);

CK_RV CK_ENTRY C_CloseSession(
	CK_SESSION_HANDLE hSession
);

CK_RV CK_ENTRY C_CloseAllSessions(
	CK_SLOT_ID slotID
);

CK_RV CK_ENTRY C_GetSessionInfo(
	CK_SESSION_HANDLE hSession,
	CK_SESSION_INFO_PTR pInfo
);

CK_RV CK_ENTRY C_GetOperationState(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pOperationState,
	CK_SIZE_PTR	pOperationStateLen
);

CK_RV CK_ENTRY C_SetOperationState(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pOperationState,
	CK_SIZE	operationStateLen,
	CK_OBJECT_HANDLE hEncryptionKey,
	CK_OBJECT_HANDLE hAuthenticationKey
);

CK_RV CK_ENTRY C_Login(
	CK_SESSION_HANDLE hSession,
	CK_USER_TYPE userType,
	CK_CHAR_PTR pPin,
	CK_SIZE pinLen
);

CK_RV CK_ENTRY C_Logout(
	CK_SESSION_HANDLE hSession
);

CK_RV CK_ENTRY C_CreateObject(
	CK_SESSION_HANDLE hSession,
	CK_ATTRIBUTE_PTR pTemplate,
	CK_COUNT count,
	CK_OBJECT_HANDLE_PTR phObject
);

CK_RV CK_ENTRY C_CopyObject(
	CK_SESSION_HANDLE hSession,
	CK_OBJECT_HANDLE hObject,
	CK_ATTRIBUTE_PTR pTemplate,
	CK_COUNT count,
	CK_OBJECT_HANDLE_PTR phNewObject
);

CK_RV CK_ENTRY C_DestroyObject(
	CK_SESSION_HANDLE hSession,
	CK_OBJECT_HANDLE hObject
);

CK_RV CK_ENTRY C_GetObjectSize(
	CK_SESSION_HANDLE hSession,
	CK_OBJECT_HANDLE hObject,
	CK_SIZE_PTR pSize
);

CK_RV CK_ENTRY C_GetAttributeValue(
	CK_SESSION_HANDLE hSession,
	CK_OBJECT_HANDLE hObject,
	CK_ATTRIBUTE_PTR pTemplate,
	CK_COUNT count
);

CK_RV CK_ENTRY C_SetAttributeValue(
	CK_SESSION_HANDLE hSession,
	CK_OBJECT_HANDLE hObject,
	CK_ATTRIBUTE_PTR pTemplate,
	CK_COUNT count
);

CK_RV CK_ENTRY C_FindObjectsInit(
	CK_SESSION_HANDLE hSession,
	CK_ATTRIBUTE_PTR pTemplate,
	CK_COUNT count
);

CK_RV CK_ENTRY C_FindObjects(
	CK_SESSION_HANDLE hSession,
	CK_OBJECT_HANDLE_PTR phObject,
	CK_COUNT maxObjectCount,
	CK_COUNT_PTR pObjectCount
);

CK_RV CK_ENTRY C_EncryptInit(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hKey
);

CK_RV CK_ENTRY C_Encrypt(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pData,
	CK_SIZE dataLen,
	CK_BYTE_PTR pEncryptedData,
	CK_SIZE_PTR pEncryptedDataLen
);

CK_RV CK_ENTRY C_EncryptUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pPart,
	CK_SIZE partLen,
	CK_BYTE_PTR pEncryptedPart,
	CK_SIZE_PTR pEncryptedPartLen
);

CK_RV CK_ENTRY C_EncryptFinal(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pLastEncryptedPart,
	CK_SIZE_PTR pEncryptedPartLen
);

CK_RV CK_ENTRY C_DecryptInit(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hKey
);

CK_RV CK_ENTRY C_Decrypt(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pEncryptedData,
	CK_SIZE encryptedDataLen,
	CK_BYTE_PTR pData,
	CK_SIZE_PTR pDataLen
);

CK_RV CK_ENTRY C_DecryptUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pEncryptedPart,
	CK_SIZE encryptedPartLen,
	CK_BYTE_PTR pPart,
	CK_SIZE_PTR pPartLen
);

CK_RV CK_ENTRY C_DecryptFinal(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pLastPart,
	CK_SIZE_PTR lastPartLen
);

CK_RV CK_ENTRY C_DigestInit(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism
);

CK_RV CK_ENTRY C_Digest(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pData,
	CK_SIZE dataLen,
	CK_BYTE_PTR pDigest,
	CK_SIZE_PTR pDigestLen
);

CK_RV CK_ENTRY C_DigestUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pPart,
	CK_SIZE partLen
);

CK_RV CK_ENTRY C_DigestKey(
	CK_SESSION_HANDLE hSession,
	CK_OBJECT_HANDLE hKey
);

CK_RV CK_ENTRY C_DigestFinal(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pDigest,
	CK_SIZE_PTR pDigestLen
);

CK_RV CK_ENTRY C_SignInit(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hKey
);

CK_RV CK_ENTRY C_Sign(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pData,
	CK_SIZE dataLen,
	CK_BYTE_PTR pSignature,
	CK_SIZE_PTR pSignatureLen
);

CK_RV CK_ENTRY C_SignUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pPart,
	CK_SIZE partLen
);

CK_RV CK_ENTRY C_SignFinal(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pSignature,
	CK_SIZE_PTR pSignatureLen
);

CK_RV CK_ENTRY C_SignRecoverInit(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hKey
);

CK_RV CK_ENTRY C_SignRecover(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pData,
	CK_SIZE dataLen,
	CK_BYTE_PTR pSignature,
	CK_SIZE_PTR pSignatureLen
);

CK_RV CK_ENTRY C_VerifyInit(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hKey
);

CK_RV CK_ENTRY C_Verify(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pData,
	CK_SIZE dataLen,
	CK_BYTE_PTR pSignature,
	CK_SIZE signatureLen
);

CK_RV CK_ENTRY C_VerifyUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pPart,
	CK_SIZE partLen
);

CK_RV CK_ENTRY C_VerifyFinal(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pSignature,
	CK_SIZE signatureLen
);

CK_RV CK_ENTRY C_VerifyRecoverInit(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hKey
);

CK_RV CK_ENTRY C_VerifyRecover(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pSignature,
	CK_SIZE signatureLen,
	CK_BYTE_PTR pData,
	CK_SIZE_PTR pDataLen
);

CK_RV CK_ENTRY C_DigestEncryptUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pInPart,
	CK_SIZE inPartLen,
	CK_BYTE_PTR pOutPart,
	CK_SIZE_PTR pOutPartLen
);

CK_RV CK_ENTRY C_DecryptDigestUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pInPart,
	CK_SIZE inPartLen,
	CK_BYTE_PTR pOutPart,
	CK_SIZE_PTR pOutPartLen
);

CK_RV CK_ENTRY C_SignEncryptUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pInPart,
	CK_SIZE inPartLen,
	CK_BYTE_PTR pOutPart,
	CK_SIZE_PTR pOutPartLen
);

CK_RV CK_ENTRY C_DecryptVerifyUpdate(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pInPart,
	CK_SIZE inPartLen,
	CK_BYTE_PTR pOutPart,
	CK_SIZE_PTR pOutPartLen
);

CK_RV CK_ENTRY C_GenerateKey(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_ATTRIBUTE_PTR pTemplate,
	CK_COUNT count,
	CK_OBJECT_HANDLE_PTR phKey
);

CK_RV CK_ENTRY C_GenerateKeyPair(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_ATTRIBUTE_PTR pPublicKeyTemplate,
	CK_COUNT publicKeyAttributeCount,
	CK_ATTRIBUTE_PTR pPrivateKeyTemplate,
	CK_COUNT privateKeyAttributeCount,
	CK_OBJECT_HANDLE_PTR phPrivateKey,
	CK_OBJECT_HANDLE_PTR phPublicKey
);

CK_RV CK_ENTRY C_WrapKey(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hWrappingKey,
	CK_OBJECT_HANDLE hKey,
	CK_BYTE_PTR pWrappedKey,
	CK_SIZE_PTR pWrappedKeyLen
);

CK_RV CK_ENTRY C_UnwrapKey(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hUnwrappingKey,
	CK_BYTE_PTR pWrappedKey,
	CK_SIZE wrappedKeyLen,
	CK_ATTRIBUTE_PTR pTemplate,
	CK_COUNT attributeCount,
	CK_OBJECT_HANDLE_PTR phKey
);

CK_RV CK_ENTRY C_DeriveKey(
	CK_SESSION_HANDLE hSession,
	CK_MECHANISM_PTR pMechanism,
	CK_OBJECT_HANDLE hBaseKey,
	CK_ATTRIBUTE_PTR pTemplate,
	CK_COUNT attributeCount,
	CK_OBJECT_HANDLE_PTR phKey
);

CK_RV CK_ENTRY C_SeedRandom(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pSeed,
	CK_SIZE seedLen
);

CK_RV CK_ENTRY C_GenerateRandom(
	CK_SESSION_HANDLE hSession,
	CK_BYTE_PTR pRandomData,
	CK_SIZE randomLen
);

CK_RV CK_ENTRY C_GetFunctionStatus(
	CK_SESSION_HANDLE hSession
);

CK_RV CK_ENTRY C_CancelFunction(
	CK_SESSION_HANDLE hSession
);

CK_RV CK_CALLBACK Notify(
	CK_SESSION_HANDLE hSession,
	CK_NOTIFICATION event,
	CK_VOID_PTR pApplication
);

#endif /* ifndef INC_PKCS11F1_H */
