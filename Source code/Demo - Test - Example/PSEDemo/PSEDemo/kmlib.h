/*
 * $Id: prod/include/kmlib.h 1.1.1.1.1.2 2010/08/10 03:12:05EDT Franklin, Brian (bfranklin) Exp  $
 * $Author: Franklin, Brian (bfranklin) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/kmlib.h $
 * $Revision: 1.1.1.1.1.2 $
 * $Date: 2010/08/10 03:12:05EDT $
 */
#ifndef KMLIB_H_INCLUDED
#define KMLIB_H_INCLUDED

#include "cryptoki.h"
#include "ctvdef.h"
#include "uicallbacks.h"

#ifdef __cplusplus
    extern "C" {        /* define as 'C' functions to prevent mangling */
#endif /* #ifdef __cplusplus */

/**
 * @mainpage KMLIB Exportable header 
 * @section global Global variables, struct declaration
 * @li @ref KM_Callbacks_t
 *
 * @section export Exportable functions
 *
 * @li @ref KM_SetCallbacks()
 * @li @ref KM_GetCallbacks()
 * @li @ref KM_GenerateSecretKey()
 * @li @ref KM_GenerateKeyPair()
 * @li @ref KM_ModifyBoolAttrs()
 * @li @ref KM_ImportFromSC()
 * @li @ref KM_ImportFromFile()
 * @li @ref KM_ImportFromScreen()
 * @li @ref KM_ImportFromPinPad()
 * @li @ref KM_ImportP12File()
 * @li @ref KM_ExportToSCwMethod()
 * @li @ref KM_ExportToSC()
 * @li @ref KM_ExportToFile()
 * @li @ref KM_ExportToFileAlg()
 * @li @ref KM_ExportToScreen()
 * @li @ref KM_DisplaySCStatus()
 * @li @ref KM_EnumerateAttributes()
 * @li @ref KM_ExportToken()
 * @li @ref KM_ImportToken()
 * @li @ref KM_ImportDomainParams()
 * @li @ref KM_EncodeECParamsP()
 * @li @ref KM_EncodeECParams2M()
 * @li @ref KM_GenerateDomainParams()
 */

 /*
  * Define the list of callbacks that will be used.
  */
typedef struct
{
    /**< To prompt for yes/no */
    UICB_PromptConfirmation_t promptConfirmation;
    /**< To prompt for PIN */
    UICB_PromptPin_t          promptPin;
    /**< To prompt for PIN using supplied string */
    UICB_PromptTokenPin_t     promptTokenPin;
    /**< To prompt for a 32-bit integer */
    UICB_PromptInt32_t        promptInt32;
    /**< To prompt for a string */
    UICB_PromptString_t       promptString;
    /**< To prompt for a key component */
    UICB_PromptKeyComponent_t promptKeyComponent;
    /**< To prompt for a smart card */
    UICB_PromptForSmartCard_t promptForSmartCard;
    /**< To dislay a message */
    UICB_ShowMsg_t            showMsg;
    /**< To display a key component */
    UICB_ShowKeyComponent_t   showKeyComponent;
    /**< To display an export header */
    UICB_ShowExportHeader_t   showExportHeader;
    /**< To display an import header */
    UICB_ShowImportHeader_t   showImportHeader;
    /**< To display info about a smart card */
    UICB_ShowSCBatchInfo_t    showSCBatchInfo;
    /**< To prompt for a choice from a list */
    UICB_ChooseFromList_t     chooseFromList;
    /**< To prompt for binary data */
    UICB_PromptForBinData_t   promptForBinData;
} KM_Callbacks_t;


/*
 * Smart Card and File key backup header types
 */

#define KMU_HDR_VER_MIN         200 /** lowest HDR version number */

#define KMU_HDR_VER             200 /** all DES3 or SC DES2 XOR: note: this is not the version of KMU */
#define KMU_HDR_VER_NOFM        400 /** DES3_NOFM: Only used by SC N of M key recovery mechanism */
#define KMU_HDR_AES_XOR         500 /** KEK file or SC XOR key recovery AES256 encryption and signing */
#define KMU_HDR_AES_NOFM        600 /** SC NOFM key recovery AES256 encryption and signing */

#define KMU_HDR_VER_MAX         600 /** highest HDR version number */

/*
 * Smart Card and File backup key recovery methods (see deriveMech)
 */

#define KM_XOR_MECHANISM  0
#define KM_NOFM_MECHANISM 1  

/*
 * Smart Card and File backup key algorithm (see algType)
 */

#define KM_ALG_DES3  CKK_DES3
#define KM_ALG_AES   CKK_AES



/*
 * API Prototypes 
 */
CK_RV KM_SetCallbacks(KM_Callbacks_t* pCallbacks);

CK_RV KM_GetCallbacks(KM_Callbacks_t* pCallbacks);

CK_RV KM_GenerateSecretKey(CK_SESSION_HANDLE hSession,
                           CK_KEY_TYPE keyType,
                           CK_SIZE keySizeInBits,
                           CK_ATTRIBUTE* pTpl,
                           CK_COUNT tplSize,
                           CK_COUNT numComps,
                           CK_OBJECT_HANDLE* phKey);

CK_RV KM_GenerateKeyPair(CK_SESSION_HANDLE hSession,
                         CK_KEY_TYPE keyType,
                         CK_SIZE keySizeInBits,
                         CK_ATTRIBUTE* pPublicKeyTpl,
                         CK_COUNT publicKeyTplSize,
                         CK_ATTRIBUTE* pPrivateKeyTpl,
                         CK_COUNT privateKeyTplSize,
                         CK_OBJECT_HANDLE* phPublicKey,
                         CK_OBJECT_HANDLE* phPrivateKey);

CK_RV KM_ModifyBoolAttrs(CK_SESSION_HANDLE hSession,
                         CK_CHAR* pUserPin,
                         CK_SIZE userPinLen,
                         CK_CHAR* pSoPin,
                         CK_SIZE soPinLen,
                         CK_OBJECT_HANDLE hObj,
                         CK_ATTRIBUTE_TYPE* pAttrs,
                         CK_COUNT numAttrs);

CK_RV KM_ImportFromSC(CK_SESSION_HANDLE hSession,
                      CK_SLOT_ID cardSlotId,
                      CK_OBJECT_HANDLE hUnwrapKey,
                      CK_ULONG importVersion);

CK_RV KM_ImportFromFile(CK_SESSION_HANDLE hSession,
                        const char* pszFileName,
                        CK_OBJECT_HANDLE hUnwrapKey,
                        CK_ULONG importVersion);


CK_RV KM_ImportFromScreen(CK_SESSION_HANDLE hSession,
                          CK_CHAR* pszLabel,
                          CK_KEY_TYPE keyType,
                          CK_SIZE keySizeInBits,
                          CK_ATTRIBUTE* pTpl,
                          CK_COUNT tplSize,
                          CK_COUNT numComps,
                          CK_OBJECT_HANDLE hUnwrapKey,
                          CK_BBOOL isEncMultiPart);

CK_RV KM_ImportFromPinPad(CK_SESSION_HANDLE hSession,
                          CK_CHAR* pszLabel,
                          CK_KEY_TYPE keyType,
                          CK_SIZE keySizeInBits,
                          CK_ATTRIBUTE* pTpl,
                          CK_COUNT tplSize,
                          CK_COUNT numComps,
                          CK_OBJECT_HANDLE* phKey);

CK_RV KM_ImportP12File(CK_SESSION_HANDLE hSession,
                       CK_CHAR* pszFileName,
                       CK_ATTRIBUTE* pPrivateKeyTpl,
                       CK_COUNT privateKeyTplSize,
                       CK_ATTRIBUTE* pCertTpl,
                       CK_COUNT certTplSize,
                       CK_OBJECT_HANDLE* phPrivateKey, 
                       CK_OBJECT_HANDLE* phCert);

CK_RV KM_ExportToP12Pbe(CK_SESSION_HANDLE        hSession,
                        CK_OBJECT_HANDLE         privKey,
                        CK_OBJECT_HANDLE         keyCert,
                        CK_MECHANISM_TYPE        safeBagKgMech,
                        CK_MECHANISM_TYPE        safeContentKgMech,
                        CK_MECHANISM_TYPE        hmacKgMech,
                        const char              *p12FileName);

/* old redundant function - see KM_ExportToSCwMethod */
CK_RV KM_ExportToSC(CK_SESSION_HANDLE hSession,
                    CK_CHAR* pUserPin,
                    CK_SIZE userPinLen,
                    CK_OBJECT_HANDLE* phWrapeeObjs,
                    CK_COUNT numWrapeeObjs,
                    CK_OBJECT_HANDLE hWrapKey,
                    CK_SLOT_ID cardSlotId);
                    
/* old redundant function - see KM_ExportToSCwMethodAlg */
CK_RV KM_ExportToSCwMethod(CK_SESSION_HANDLE hSession,
                           CK_CHAR* pUserPin,
                           CK_SIZE userPinLen,
                           CK_OBJECT_HANDLE* phWrapeeObjs,
                           CK_COUNT numWrapeeObjs,
                           CK_OBJECT_HANDLE hWrapKey,
                           CK_SLOT_ID cardSlotId,
                           CK_ULONG deriveMech
                           );
                                        
CK_RV KM_ExportToSCwMethodAlg(CK_SESSION_HANDLE hSession,
                           CK_CHAR* pUserPin,
                           CK_SIZE userPinLen,
                           CK_OBJECT_HANDLE* phWrapeeObjs,
                           CK_COUNT numWrapeeObjs,
                           CK_OBJECT_HANDLE hWrapKey,
                           CK_SLOT_ID cardSlotId,
                           CK_ULONG deriveMech,
                           int algType);
                                        

CK_RV KM_ExportToFile(CK_SESSION_HANDLE hSession,
                      CK_OBJECT_HANDLE* phWrapeeObjs,
                      CK_COUNT numWrapeeObjs,
                      CK_OBJECT_HANDLE hWrapKey,
                      const char* pszFileName);

CK_RV KM_ExportToFileAlg(CK_SESSION_HANDLE hSession,
                      int algType,
                      CK_OBJECT_HANDLE* phWrapeeObjs,
                      CK_COUNT numWrapeeObjs,
                      CK_OBJECT_HANDLE hWrapKey,
                      const char* pszFileName);


CK_RV KM_ExportToScreen(CK_SESSION_HANDLE hSession,
                        CK_OBJECT_HANDLE hWrapeeKey,
                        CK_OBJECT_HANDLE hWrapKey,
                        CK_BBOOL isEncMultiPart);

CK_RV KM_DisplaySCStatus(CK_SLOT_ID cardSlot);

CK_RV KM_EnumerateAttributes(CK_SESSION_HANDLE hSession,
                             CK_OBJECT_HANDLE hObj,
                             CK_ATTRIBUTE* pTpl,
                             CK_SIZE* pTplSize);

CK_RV KM_ExportToken(CK_SESSION_HANDLE hSession,
                CK_CHAR serialNumber[CK_SERIAL_NUMBER_SIZE],
                CK_BYTE* tokenData,
                CK_ULONG* pTokenDataSize);

CK_RV KM_ImportToken(CK_SESSION_HANDLE hSession,
                const CK_BYTE* tokenData,
                CK_ULONG tokenDataLen);

CK_RV KM_EncodeECParamsP(
						CK_BYTE_PTR prime,    CK_SIZE primeLen,
						CK_BYTE_PTR curveA,   CK_SIZE curveALen,
						CK_BYTE_PTR curveB,   CK_SIZE curveBLen,
						CK_BYTE_PTR curveSeed,CK_SIZE curveSeedLen,
						CK_BYTE_PTR baseX,    CK_SIZE baseXLen,
						CK_BYTE_PTR baseY,    CK_SIZE baseYLen,
						CK_BYTE_PTR bpOrder,  CK_SIZE bpOrderLen,
						CK_BYTE_PTR cofactor, CK_SIZE cofactorLen,
						CK_BYTE_PTR result,   CK_SIZE * resultLen
				);


typedef enum {
    ECBT_GnBasis,   /* parameters = 0,  0,  0 */
    ECBT_TpBasis,   /* parameters = k,  0,  0 */
    ECBT_PpBasis    /* parameters = k1, k2, k3 */
} ECBasisType;

CK_RV KM_EncodeECParams2M(
						CK_SIZE m,
                        ECBasisType basis,
                        CK_SIZE  parameters[3],   
						CK_BYTE_PTR curveA,   CK_SIZE curveALen,
						CK_BYTE_PTR curveB,   CK_SIZE curveBLen,
						CK_BYTE_PTR curveSeed,CK_SIZE curveSeedLen,
						CK_BYTE_PTR baseX,    CK_SIZE baseXLen,
						CK_BYTE_PTR baseY,    CK_SIZE baseYLen,
						CK_BYTE_PTR bpOrder,  CK_SIZE bpOrderLen,
						CK_BYTE_PTR cofactor, CK_SIZE cofactorLen,
						CK_BYTE_PTR result,   CK_SIZE * resultLen
				);


CK_RV  KM_ImportDomainParams( 
                       CK_SESSION_HANDLE hSession,
                       CK_CHAR* pin,                /* optional - callback if required and not provided */
                       CK_SIZE userPinLen,
                       CK_ATTRIBUTE* pObjTpl,       /* CLASS, LABEL, MODIFIABLE, PRIVATE, KEY_TYPE, DELETABLE, TOKEN */
                       CK_COUNT ObjTplSize,
                       char * filename,
                       CK_OBJECT_HANDLE* phObj );

CK_RV KM_GenerateDomainParams(CK_SESSION_HANDLE hSession,
                           CK_KEY_TYPE keyType,
                           CK_SIZE keySizeInBits,
                           CK_ATTRIBUTE* pTpl,
                           CK_COUNT tplSize,
                           CK_OBJECT_HANDLE* phKey);
 

#ifdef __cplusplus
}
#endif /* #ifdef __cplusplus */

#endif
