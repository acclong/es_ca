
#ifndef UICALLBACKS_H_INCLUDED
#define UICALLBACKS_H_INCLUDED

#include "cryptoki.h"

typedef enum
{
    UICB_CR_NO = 0,
    UICB_CR_YES    
} UICB_ConfirmResult_t;


typedef enum
{
    UICB_MT_DEBUG = 0,
    UICB_MT_INFO,
    UICB_MT_WARN,
    UICB_MT_ERROR
} UICB_MsgType_t;

/**
 *
 * This function will prompt the user for confirmation of an action, as 
 * specified in pszMessage. If cancellable is TRUE, user is given the 
 * option to cancel, as well as YES/NO. The choice is returned in pResult.
 *
 * @param pszMessage
 *  Message used to prompt the user for their choice
 *
 * @param bCancellable
 *  Specifies whether to give user the option to cancel
 *
 * @param pResult
 *  Choice, as selected by the user
 */
typedef CK_RV (*UICB_PromptConfirmation_t)(const char* pszMessage,
                                           CK_BBOOL bCancellable,
                                           UICB_ConfirmResult_t* pResult);


/**
 *
 * This function will prompt the user for the PIN of the specified user on
 * the given slot. The result is returned in the pPin parameter. The number of
 * CK_CHARs copied into pPin is returned in pPinLen, which is also used as 
 * input to the function. If the number of CK_CHARs entered is larger than 
 * the number passed in in pPinLen, the function will return 
 * CKR_BUFFER_TOO_SMALL and the required size will be returned in pPinLen.
 *
 * @param slotId
 *  Id of the slot for which to prompt for the PIN
 *
 * @param fConfirm
 *  Flag to indicate whether or not to confirm entered PIN.
 *
 * @param userType
 *  User whose PIN should be prompted for.
 *
 * @param pPin
 *  Pointer to buffer to accept PIN as entered by the user
 *
 * @param pPinLen
 *  Pointer to a CK_ULONG specifying the length of the buffer. Upon successful
 *  completion of the function, the CK_ULONG will contain either the number of 
 *  bytes copied into pPin, or the length required to hold the entered 
 *  PIN.
 */
typedef CK_RV (*UICB_PromptPin_t)(CK_SLOT_ID slotId,
                                  CK_BBOOL fConfirm,
                                  CK_USER_TYPE userType,
                                  CK_CHAR* pPin,
                                  CK_ULONG* pPinLen);

/**
 *
 * This function will display the prompt in the pszMessage parameter and then
 * wait for the user to enter the PIN. The result is returned in
 * the pPin parameter. The number of CK_CHARs copied into pPin is returned in
 * pPinLen, which is also used as input to the function. If the number of
 * CK_CHARs entered is larger than the number passed in in pPinLen, the
 * function will return CKR_BUFFER_TOO_SMALL and the required size will be
 * returned in pPinLen.
 *
 * @param pszMessage
 *  The message to display when prompting for the PIN.
 *
 * @param pPIN
 *  Pointer to buffer to accept PIN as entered by the user
 *
 * @param pPinLen
 *  Pointer to a CK_ULONG specifying the length of the buffer. Upon successful
 *  completion of the function, the CK_ULONG will contain either the number of 
 *  bytes copied into pPin, or the length required to hold the entered 
 *  PIN.
 */
typedef CK_RV (*UICB_PromptTokenPin_t)(const char* pszMessage,
                                       CK_CHAR *pPIN,
                                       CK_ULONG* pPinLen);


/**
 *
 * This function will prompt the user for an integer, as specified in 
 * pszMessage. The number entered is returned in pInt.
 *
 * @param pszMessage
 *  Message used to prompt the user for the integer.
 *
 * @param pUlong
 *  Pointer to a CK_ULONG, which upon successful completion of this function,
 *  will contain the integer as entered by the user.
 */
typedef CK_RV (*UICB_PromptInt32_t)(const char* pszMessage,
                                    CK_ULONG* pUlong);


/**
 *
 * This function will prompt the user for a string, as specified in 
 * pszMessage. The result is returned in the pszBuf parameter. The number of
 * CK_CHARs copied into pszBuf is returned in pBufLen, which is also used as 
 * input to the function. If the number of CK_CHARs entered is larger than 
 * the number passed in in pBufLen, the function will return 
 * CKR_BUFFER_TOO_SMALL and the required size will be returned in pBufLen.
 *
 * @param pszMessage
 *  Message used to prompt the user for the string.
 *
 * @param pBuf
 *  Pointer to buffer to accept string as entered by the user.
 *
 * @param pBufLen
 *  Pointer to the length of the buffer to accept the entered string. Upon 
 *  successful completion of the function, the CK_ULONG will contain either 
 *  the number of bytes copied into pPin, or the length required to hold the 
 *  entered string.
 */
typedef CK_RV (*UICB_PromptString_t)(const char* pszMessage,
                                     char* pBuf,
                                     CK_ULONG* pBufLen);


/**
 * Prompt for a key component from the user and verify its KCV.
 *
 * @Note: It is up to the library to convert the hex string entered as the
 * key component to binary.
 *
 * @param compNum
 *  The number of the key component to get.
 *
 * @param numComps
 *  The number of key components to get.
 *
 * @param pKeyComp
 *  Location to store the verified key component.
 * 
 * @param pKeyCompLen
 *  Location to store the length of the key component.
 */
typedef CK_RV (*UICB_PromptKeyComponent_t)(int compNum,
                                           int numComps,
                                           char* pKeyComp,
                                           int* pKeyCompLen);


/**
 * Prompt and wait for a smart card to be entered in the specified slot.
 *
 * @param cardSlotId
 *  Id of the slot to wait for the smart card.
 *
 * @param pszMessage
 *  The message to display.
 *
 * @param pszPrompt
 *  The prompt to display.
 */
typedef CK_RV (*UICB_PromptForSmartCard_t)(CK_SLOT_ID cardSlotId,
                                           const char* pszMessage,
                                           const char* pszPrompt);


/**
 *
 * This function will display a message to the user as specified in 
 * pszMessage. 
 *
 * @param pszMessage
 *  Message to display
 *
 * @param msgType
 *  Type of message to display
 */
typedef void (*UICB_ShowMsg_t)(UICB_MsgType_t msgType,
                               const char* pszMessage);


/**
 * Display a key component and KCV.
 *
 * @param compNum
 *  The current key component number.
 *
 * @param numComps
 *  The number of components to display.
 *
 * @param pKeyComp
 *  The key component to display.
 *
 * @param pKCV
 *  The KCV of the component to display.
 */
typedef CK_RV (*UICB_ShowKeyComponent_t)(int compNum,
                                         int numComps,
                                         const char* pKeyComp,
                                         const char* pKCV);

/**
 * Show the current smart card batch name and user number.
 *
 * @param pszBatchName
 *  The name of the current batch.
 *
 * @param custodianNumber
 *  Number of the current custodian.
 */
typedef void (*UICB_ShowExportHeader_t)(const char* pszBatchName,
                                        int custodianNumber);


/**
 * Show the current smart card batch name and user number.
 *
 * @param pszBatchName
 *  The name of the current batch.
 *
 * @param pszUserName
 *  The name of the user who owns the card.
 */
typedef void (*UICB_ShowImportHeader_t)(const char* pszBatchName,
                                        const char* pszUserName);

/**
 * Show smart card batch info
 *
 * @param pszBatchName 
 *  Name of the batch that this card belongs to
 *
 * @param timeCreated
 *  Time this card was created
 *
 * @param cardNumber
 *  Card number within the batch of this card 
 *
 * @param numCustodians
 *  Number of custodians within the batch
 *
 * @param custodianNumber
 *  Number of this custodian
 *
 * @param pszUserName
 *  Name of the user that 'owns' this card
 *
 * @param percentageOfEks
 *  Percentage of the entire eks that has been stored on this card
 */
typedef CK_RV (*UICB_ShowSCBatchInfo_t)(const char* pszBatchName,
                                        const unsigned long timeCreated,
                                        const unsigned long cardNumber,
                                        const unsigned long numCustodians,
                                        const unsigned long custodianNumber,
                                        const char* pszUserName,
                                        const double percentageOfEks);

/**
 * @brief offer a list for the user to choose from
 *
 * @param prompt
 *  title for the list
 *
 * @param items
 *  Array of strings describing each option
 *
 * @param numItems
 *  The number of options
 *
 * @return index selected or -1 if error
 */
typedef int (*UICB_ChooseFromList_t)( const char * prompt,
					const char** items,
                    int numItems
                  );

/**
 * callback to fetch binary data
 */
typedef CK_RV (*UICB_PromptForBinData_t)(const char* pszMessage, /* prompt */
                        const char* xmlTag, /* optional xml tag */
						int minLen, int maxLen,/* range of length */
                        CK_COUNT* pOutLen, /* OUT: bytes returned */
						CK_CHAR ** data	/* OUT: user must free */
						);

#endif   /* UICALLBACKS_H_INCLUDED */

