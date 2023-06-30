/**************************************************************************

    Copyright (C) 2004 Eracom Technologies Australia Pty. Ltd.
    All Rights Reserved 

    Use of this file for any purpose whatsoever is prohibited without the
    prior written consent of Eracom Technologies Australia Pty. Ltd.

    File  : SignVerifyKP.c
 
    Description:
    
    Sample program to demonstrate how to sign some raw data using a private
    key and verify the signature using the corresponding public key.


    Version Control Info:

    $Revision: 1.1 $
    $Date: 2009/01/27 10:02:43EST $
    $Author: Sorokine, Joseph (jsorokine) $

**************************************************************************/
#include <stdafx.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

#include "cryptoki.h"
#include "ctvdef.h"
#include "ctutil.h"

/** 
 * This macro is used to check the return value of a function and print an 
 * error message and jump to a label if the value is not CKR_OK. Using it
 * reduces the complexity of code within a function.
 */
#define CHECK_CK_RV_GOTO(rv, string, label)                 \
    if (rv != CKR_OK)                                       \
    {                                                       \
        fprintf(stderr, "Error occured : %s\n", string);    \
        goto label;                                         \
    }


/* ****************************************************************************
 *
 *  P R I V A T E   F U N C T I O N   P R O T O T Y P E S 
 *
 * ***************************************************************************/

/**
 * Search for an object, using the given session.
 *
 * @param hSession 
 *  Session to use to perform the search. If searching for a private 
 *  object, it is up to the caller to perform the C_Login call.
 *
 * @param objClass
 *  Type of the object being searched for.
 *
 * @param pObjLabel
 *  Label of the object to search for
 *
 * @param phObj
 *  Pointer to the handle, which, upon successful completion of the function,
 *  will contain a handle to the object.
 */
static CK_RV FindObject(CK_SESSION_HANDLE hSession,
                        CK_OBJECT_CLASS objClass,
                        CK_CHAR* pObjLabel,
                        CK_OBJECT_HANDLE* phObj);

/**
 * Sign the given buffer of data, using the given key and storing the 
 * signature in the given buffer.
 *
 * @param hSession
 *  Session to use to perform the sign operation
 *
 * @param hKey
 *  Handle to the key to use to perform the sign operation
 *
 * @param pData
 *  Data to sign
 *
 * @param dataLen
 *  Length of the data that needs to be signed
 *
 * @param ppSignature
 *  Buffer used to store the resulting signature. Memory must be allocated by
 *  the caller.
 *
 * @param pSignatureLen
 *  Pointer to a CK_SIZE containing the length of the signature buffer. Upon 
 *  successful completion of the function, this parameter will contain the 
 *  amount of data copied into the signature buffer, or the size of the 
 *  buffer required to hold the signature if the given buffer was too small.
 */
static CK_RV SignData(CK_SESSION_HANDLE hSession,
                      CK_OBJECT_HANDLE hKey,
                      CK_CHAR* pData,
                      CK_SIZE dataLen,
                      CK_CHAR** ppSignature,
                      CK_SIZE* pSignatureLen);

/**
 * Verify a given buffer of data against a signature
 *
 * @param hSession
 *  Session to use to perform the verify operation
 *
 * @param hKey
 *  Handle to the key to use to perform the verify operation
 *
 * @param pData
 *  Data to verify the signature against
 *
 * @param dataLen
 *  Length of the data 
 *
 * @param pSignature
 *  Signature to verify the data against
 *
 * @param signatureLen
 *  Length of the signature to verify
 */
static CK_RV VerifyData(CK_SESSION_HANDLE hSession,
                        CK_OBJECT_HANDLE hKey,
                        CK_CHAR* pData,
                        CK_SIZE dataLen,
                        CK_CHAR* pSignature,
                        CK_SIZE signatureLen);

static void usage(void)
{
    printf("\nsignverifykp [-?] [-s<SlotId>] -p<PublicKey> -q<PrivateKey>");
    printf("\n");
    printf("\n-?            HSM Demo");
    printf("\n-s            id of the slot to initialise a token in");
    printf("\n-p            the name of the public key to verify with");
    printf("\n-q            the name of the private key to sign with");
    printf("\n");
    exit(0);
}


/* ****************************************************************************
 *
 *  G L O B A L   D A T A 
 *
 * ***************************************************************************/

CK_CHAR g_dataToSign[] = 
    { 'T', 'h', 'i', 's', ' ', 'i', 's', ' ',
      's', 'o', 'm', 'e', ' ', 'd', 'a', 't',
      'a', ' ', 't', 'o', ' ', 's', 'i', 'g',
      'n', ' ', '.', '.', '.', ' ', ' ', '\0' };
//
//CK_CHAR* g_dataToSign;


CK_CHAR userPin[]="123456";

///////////////////////////////123456789


/* ****************************************************************************
 *
 *  M A I N    F U N C T I O N 
 *
 * ***************************************************************************/
int main(int argc, char **argv)
{
    CK_RV rv = CKR_OK;

    char* pArg = NULL;
    char* pValue = NULL;

    CK_SESSION_HANDLE hSession = CK_INVALID_HANDLE;
    CK_OBJECT_HANDLE hPubKey = CK_INVALID_HANDLE; 
    CK_OBJECT_HANDLE hPriKey = CK_INVALID_HANDLE;
    
    CK_SLOT_ID slotId = 0;
    CK_CHAR* pPubKeyName = NULL;
    CK_CHAR* pPriKeyName = NULL;

    CK_CHAR* pSignature = NULL;
    CK_SIZE signatureLen = 0;

    int i = 0;
	printf("Starting ......");
    /*
     * Process command line arguments
     */
#define GET_VALUE                       \
            if (pArg[1] == '\0')        \
            {                           \
                if (++i < argc)         \
                {                       \
                    pValue = argv[i];   \
                }                       \
                else                    \
                {                       \
                    usage();            \
                }                       \
            }                           \
            else                        \
            {                           \
                pValue = pArg+1;        \
            }

    for (i = 1; i < argc; ++i)
    {
        if (argv[i][0] == '-')
        {
            pArg = &argv[i][1];

            switch (toupper((int)*pArg))
            {
                case '?':
                    usage();
                break;

                case 'S':
                    GET_VALUE;
                    slotId = atoi(pValue);
                break;

                case 'P':
                    GET_VALUE;
                    pPubKeyName = (CK_CHAR*)pValue;
                break;

                case 'Q':
                    GET_VALUE;
                    pPriKeyName = (CK_CHAR*)pValue;
                break;
            }
        }
    }

    /* check user input */
    if (pPubKeyName == NULL)
    {
        printf("\n\nNo public key was specified\n");
        usage();
    }

    if (pPriKeyName == NULL)
    {
        printf("\n\nNo private key was specified\n");
        usage();
    }

    /* Initialise the cryptoki API */
    rv = C_Initialize(NULL);
    CHECK_CK_RV_GOTO(rv, "C_Initialize", end);

    /* Obtain a session so we can perform cryptoki operations */
    rv = C_OpenSession(slotId, CKF_RW_SESSION, NULL, NULL, &hSession);
    CHECK_CK_RV_GOTO(rv, "C_OpenSession", end);

	rv = C_Login(hSession,CKU_USER,userPin,strlen((char*)userPin));
    /* 
     * Find the private key to use for the sign operation
     */
    printf("Finding a private key to sign ... ");

    rv = FindObject(hSession, 
                    CKO_PRIVATE_KEY,
                    pPriKeyName,
                    &hPriKey);
    CHECK_CK_RV_GOTO(rv, "FindObject", end);

    printf("Got key\n");

    printf("Data (as string) = %s\n", g_dataToSign);

    /*
     * Perform the sign operation
     */
    printf("Performing the sign operation ... ");

    rv = SignData(hSession, 
                  hPriKey,
                  g_dataToSign, 
                  strlen((char*)g_dataToSign), 
                  &pSignature, 
                  &signatureLen);
    CHECK_CK_RV_GOTO(rv, "SignData", end);

    printf("done!\n");

    /*
     * Perform the verify operation
     */
    printf("Performing the verify operation ... ");

    rv = FindObject(hSession, 
                    CKO_PUBLIC_KEY,
                    pPubKeyName,
                    &hPubKey);
    CHECK_CK_RV_GOTO(rv, "FindObject", end);

    rv = VerifyData(hSession, 
                    hPubKey, 
                    g_dataToSign, 
                    strlen((char*)g_dataToSign), 
                    pSignature, 
                    signatureLen);
    CHECK_CK_RV_GOTO(rv, "VerifyData", end);

    printf("done!\n");

    /* We've finished our work, close the session */
    rv = C_CloseSession(hSession);
    CHECK_CK_RV_GOTO(rv, "C_CloseSession", end);

    /* We no longer need the cryptoki API ... */
    rv = C_Finalize(NULL);
    CHECK_CK_RV_GOTO(rv, "C_Finalize", end);

end:

    if (pSignature != NULL)
    {
        free(pSignature);
        pSignature = NULL;
    }

    if (rv != CKR_OK)
    {
        fprintf(stderr,
                "Error performing sign / verify operation : 0x%lx\n",
                rv);

        /*
         * Clean up... we don't care if there are any errors.
         */
        if (hSession != CK_INVALID_HANDLE) C_CloseSession(hSession);

        C_Finalize(NULL);
    }
    else
    {
        printf("Successfully performed sign / verify operation.\n");
    }

    return rv;
}

/* ****************************************************************************
 *
 *  P R I V A T E   F U N C T I O N S 
 *
 * ***************************************************************************/
static CK_RV FindObject(CK_SESSION_HANDLE hSession,
                        CK_OBJECT_CLASS objClass,
                        CK_CHAR* pObjLabel,
                        CK_OBJECT_HANDLE* phObj)
{
    CK_RV rv = CKR_OK;

    /* This is the template used to search for the object. The C_FindObjects 
     * call matches all objects that have attributes matching all attributes 
     * within the search template.
     * 
     * The attributes in the search template are : 
     *  CKA_CLASS - Points to the objClass variable which contains the value
     *              CKO_SECRET_KEY, meaning this object is a secret key object.
     *  CKA_LABEL - Points to a char array containing what will be the label
     *              of the data object.
     *
     * The search will hit on all objects with the given class and label. Note
     * that it is possible to have multiple objects on a token with matching 
     * attributes, no matter what the attributes are. There is nothing 
     * precluding the existence of duplicate objects. In the case of duplicate
     * objects, the first one found is returned 
     */
    CK_ATTRIBUTE objectTemplate[] = 
    {
        {CKA_CLASS,         NULL,       0},
        {CKA_LABEL,         NULL,       0},
    };
    CK_SIZE templateSize = sizeof(objectTemplate) / sizeof(CK_ATTRIBUTE);

    CK_ULONG numObjectsToFind = 1;
    CK_ULONG numObjectsFound = 0;

    CK_ATTRIBUTE* pAttr = NULL;

    /* 
     * Fill out the template with the values to search for
     */

    /* First set the object class ... */
    pAttr = FindAttribute(CKA_CLASS, objectTemplate, templateSize);
    pAttr->pValue = &objClass;
    pAttr->ulValueLen = sizeof(CK_OBJECT_CLASS);

    /* Now set the label ... */
    pAttr = FindAttribute(CKA_LABEL, objectTemplate, templateSize);
    pAttr->pValue = pObjLabel;
    pAttr->ulValueLen = strlen((char*)pObjLabel);

    /* 
     * Now perform the search 
     */

    /* First initialise the search operation */
    rv = C_FindObjectsInit(hSession, objectTemplate, templateSize);
    CHECK_CK_RV_GOTO(rv, "C_FindObjectsInit", end);

    /* Search */
    rv = C_FindObjects(hSession,
                       phObj,
                       numObjectsToFind,
                       &numObjectsFound);
    CHECK_CK_RV_GOTO(rv, "C_FindObjects", end);

    /* Terminate the search */
    rv = C_FindObjectsFinal(hSession);
    CHECK_CK_RV_GOTO(rv, "C_FindObjects", end);

    /* Check to see if we found a matching object */
    if (numObjectsFound == 0)
    {
        fprintf(stderr, "Object not found.\n");
        rv = CKR_GENERAL_ERROR;
    }

end:
    return rv;
}

static CK_RV SignData(CK_SESSION_HANDLE hSession,
                      CK_OBJECT_HANDLE hKey,
                      CK_CHAR* pData,
                      CK_SIZE dataLen,
                      CK_CHAR** ppSignature,
                      CK_SIZE* pSignatureLen)
{

    CK_RV rv = CKR_OK;

    /* This is the mechanism used to sign data with RSA private key. The 
     * fields of the structure are : 
     *  CKM_RSA_PKCS  - Type of the mechanism. This informs the cryptoki 
     *                  library that we want to use the RSA_PKCS algorithm 
     *                  to perform the signature operation.
     *  NULL          - This field is the parameter field. Some mechanisms 
     *                  require certain parameters to perform their functions.
     *                  CKM_RSA_PKCS does not require a parameter, hence the 
     *                  NULL value.
     *  0             - This field is the parameter length field. Since 
     *                  this mechanism type does not require a parameter,
     *                  0 is passed in as the length.
     */
    CK_MECHANISM signMech = {CKM_RSA_PKCS, NULL, 0};

    /* Initialise the sign operation */
    rv = C_SignInit(hSession, &signMech, hKey);
    CHECK_CK_RV_GOTO(rv, "C_SignInit", end);

    /* Do a length prediction so we allocate enough memory for the signature */
    rv = C_Sign(hSession, pData, dataLen, NULL, pSignatureLen);
    CHECK_CK_RV_GOTO(rv, "C_Sign", end);
    
    *ppSignature = (CK_CHAR*)malloc(*pSignatureLen);
    if (*ppSignature == NULL) return CKR_HOST_MEMORY;

    /* Do the proper sign */
    rv = C_Sign(hSession, pData, dataLen, *ppSignature, pSignatureLen);
    CHECK_CK_RV_GOTO(rv, "C_Sign", end);

    /*
     * Note that the CKM_RSA_PKCS mechanism (as well as other RSA sign/verify
     * mechanisms) can only be part of a single-part sign operation. This
     * means that it CANNOT be called with C_SignUpdate or C_SignFinal.
     */

end:

    return rv;
}

static CK_RV VerifyData(CK_SESSION_HANDLE hSession,
                        CK_OBJECT_HANDLE hKey,
                        CK_CHAR* pData,
                        CK_SIZE dataLen,
                        CK_CHAR* pSignature,
                        CK_SIZE signatureLen)
{

    CK_RV rv = CKR_OK;

    /* This is the mechanism used to verify a signature with a public key 
     * key. The fields of the structure are : 
     *  CKM_RSA_PKCS  - Type of the mechanism. This informs the cryptoki 
     *                  library that we want to use the RSA_PKCS algorithm 
     *                  to verify the signature.
     *  NULL          - This field is the parameter field. Some mechanisms 
     *                  require certain parameters to perform their functions.
     *                  CKM_RSA_PKCS does not require a parameter, hence the 
     *                  NULL value.
     *  0             - This field is the parameter length field. Since 
     *                  this mechanism type does not require a parameter,
     *                  0 is passed in as the length.
     */
    CK_MECHANISM verifyMech = {CKM_RSA_PKCS, NULL, 0};

    /* Initialise the sign operation */
    rv = C_VerifyInit(hSession, &verifyMech, hKey);
    CHECK_CK_RV_GOTO(rv, "C_VerifyInit", end);

    /* Perform the sign operation */
    rv = C_Verify(hSession, pData, dataLen, pSignature, signatureLen);
    CHECK_CK_RV_GOTO(rv, "C_Verify", end);

    /*
     * Note that the CKM_RSA_PKCS mechanism (as well as other RSA sign/verify
     * mechanisms) it can only be part of a single-part verify operation. This
     * means that it CANNOT be called with C_VerifyUpdate or C_VerifyFinal.
     */

end:

    return rv;	
}

