/**
 * @file
 * Contains definitions of the Cprov v3.x Management library.
 */
/*
 * $Id: prod/include/cmlib.h 1.1.1.1.1.1 2010/01/29 14:37:31EST Belem,Karim (kbelem) Exp  $
 * $Author: Belem,Karim (kbelem) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/cmlib.h $
 * $Revision: 1.1.1.1.1.1 $
 * $Date: 2010/01/29 14:37:31EST $
 */

#ifndef _CMLIB_H
#define _CMLIB_H

#include "cryptoki.h"

#ifdef __cplusplus
extern "C" {                /* define as 'C' functions to prevent mangling */
#endif

/**
 * Used to reference a slot within a device.
 */
typedef struct
{
    int deviceNumber;           /**< zero based device number */
    int slotIndex;              /**< zero based index within device */
} CM_SlotRef;


/**
 * Various retrievable information.
 */
typedef enum
{
    CM_MODEL = 0,               /**< Adapter model. */
    CM_BATCH,                   /**< Adapter batch. */
    CM_DATE_OF_MANUFACTURE,     /**< Date of manufacture. */
    CM_SERIAL_NUMBER,           /**< Adapter serial number. */
    CM_SECURITY_MODE,           /**< Current security mode.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit value is going to be the
                                  logical OR of various
                                  security CKF_XXX flags.
                                 */
    CM_TRANSPORT_MODE,          /**< Current transport mode.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is one of:
                                  @li CK_NO_TRANSPORT_MODE
                                  @li CK_SINGLE_TRANSPORT_MODE
                                  @li CK_CONTINUOUS_TRANSPORT_MODE
                                 */
    CM_CLOCK_LOCAL,             /**< Current time of adapter clock (LOCAL) in the
                                  format "hh:mm:ss DD/MM/YYYY (TimeZone)".  If 
                                  the clock has not been set, then 
                                  "UNAVAILABLE" is returned and CM_SyncClock or
                                  CM_SetClock should be called.
                                 */
    CM_BOARD_REVISION,          /**< Board revision. */
    CM_FIRMWARE_REVISION,       /**< Firmware revision. */
    CM_CPROV_REVISION,          /**< Cprov revision. */
    CM_BATTERY_STATUS,          /**< Battery status: "LOW" or "GOOD".
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is one of:
                                  @li 0 - Battery is low
                                  @li nonzero - Battery is good.
                                 */
    CM_PCB_VERSION,             /**< PCB version. */
    CM_FPGA_VERSION,            /**< FPGA version. */
    CM_EXTERNAL_PINS,           /**< External input pin states.
                                  This constant can be used in CM_GetInfoLong()
                                  function. Th 32-bit output has its 28 most
                                  significant bits set to 0, and the 4 least
                                  significant bits representing the state of
                                  external pins.
                                 */
    CM_FREE_MEMORY,             /**< Adapters heap space (RAM) available.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either the
                                  free memory, or CK_UNAVAILABLE_INFORMATION to
                                  indicate that the information is not
                                  available.
                                 */
    CM_TOTAL_PUBLIC_MEMORY,     /**< Total secure memory.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either the
                                  total memory, or CK_UNAVAILABLE_INFORMATION
                                  to indicate that the information is not
                                  available.
                                 */
    CM_FREE_PUBLIC_MEMORY,      /**< Available secure memory.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either the
                                  free memory, or CK_UNAVAILABLE_INFORMATION to
                                  indicate that the information is not
                                  available.
                                 */
    CM_TOTAL_SESSION_COUNT,     /**< Number of sessions open on all devices.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either the
                                  session count, or CK_UNAVAILABLE_INFORMATION
                                  to indicate that the information is not
                                  available.
                                 */
    CM_DEVICE_COUNT,            /**< Number of active devices.
                                  This constant can be used in CM_GetInfoLong()
                                  function.
                                 */
    CM_SLOT_COUNT,              /**< Number of slots on a device.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either the
                                  slot count, or CK_UNAVAILABLE_INFORMATION to
                                  indicate that the information is not
                                  available.
                                 */

    CM_TOKEN_NAME,              /**< Name (label) of a token, optionally
                                  prefixed with "<removable>" or "<admin>".
                                  "<uninitialised>" if the token has not been
                                  initialised.  <i>itemNumber</i> is the index
                                  of the slot within the device, from 0 to
                                  CM_SLOT_COUNT-1.
                                 */
    CM_EVENT_LOG_COUNT,         /**< Number of entries in device event log.
                                  This constant can be used in CM_GetInfoLong()
                                  function.
                                 */
    CM_EVENT_LOG_FULL,          /**< "TRUE" or "FALSE".
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either TRUE or
                                  FALSE.
                                 */
    CM_DEVICE_INITIALISED,      /**< "TRUE" or "FALSE".
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either TRUE or
                                  FALSE.
                                 */
    CM_APPLICATION_COUNT,       /**< Number of applications currently using cryptoki.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either the
                                  application count, or
                                  CK_UNAVAILABLE_INFORMATION to indicate that
                                  the information is not available.
                                 */
    CM_TOKEN_SESSION_COUNT,     /**< The total number of sessions open on the
                                  specified token.
                                  <i>itemNumber</i> is the index of the slot
                                  within the device, from 0 to CM_SLOT_COUNT-1.
                                  This constant can be used in CM_GetInfoLong()
                                  function.
                                 */
    CM_FM_LABEL,                /**< Label of the FM inside the device. Empty
                                  string if the device is not FM-enabled, or
                                  there are no FMs in the device (not when
                                  there is an FM, and it is disabled).
                                 */
    CM_FM_VERSION,              /**< Version of the FM inside the device. Empty
                                  string if the device is not FM-enabled, or
                                  there are no FMs in the device (not when
                                  there is an FM, and it is disabled).
                                 */
    CM_FM_MANUFACTURER,         /**< Manufacturer of the FM inside the device.
                                  Empty string if the device is not FM-enabled,
                                  or there are no FMs in the device (not when
                                  there is an FM, and it is disabled).
                                 */
    CM_FM_BUILD_TIME,           /**< Build time of the FM inside the device.
                                  Empty string if the device is not FM-enabled,
                                  or there are no FMs in the device (not when
                                  there is an FM, and it is disabled).
                                 */
    CM_FM_FINGERPRINT,          /**< Fingerprint (a hexadecimal string
                                  identifying the FM image) of the FM inside
                                  the device. Empty string if the device is not
                                  FM-enabled, or there are no FMs in the device
                                  (not when there is an FM, and it is
                                  disabled).
                                 */
    CM_FM_ROM_SIZE,             /**< Amount of ROM the FM is occupying inside
                                  the device. Returns "0" if the device is not
                                  FM-enabled, or there are no FMs in the device
                                  (not when there is an FM, and it is
                                  disabled).
                                  This constant can be used in CM_GetInfoLong()
                                  function.
                                 */
    CM_FM_RAM_SIZE,             /**< Amount of static RAM the FM is using inside
                                  the device (the actual amount of RAM used may
                                  be higher, due to dynamic memory
                                  allocations). Returns "0" if the device is
                                  not FM-enabled, there are no FMs in the
                                  device, or when there is an FM, and it is
                                  disabled).
                                  This constant can be used in CM_GetInfoLong()
                                  function.
                                 */
    CM_FM_STATUS,               /**< One of the following:
                                  @li "Enabled" - Device contains a FM, and it
                                  is active.
                                  @li "Disabled" - Device contains a FM, and it
                                  is not active.
                                  @li "No FM" - Device does not contain a FM.
                                  @li EmptyString - Device does not allow FMs.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is one of:
                                  @li CM_FM_ENABLED
                                  @li CM_FM_DISABLED
                                  @li CM_NO_FM_LOADED
                                  @li CM_NO_FM_SUPPORT
                                 */
    CM_DEVICE_ALLOWS_FM,        /**< One of the following:
                                  @li "TRUE" if it is possible to download a FM
                                  to the device.
                                  @li "FALSE" if it is not possible to download
                                  a FM to the device.
                                  This constant can be used in CM_GetInfoLong()
                                  function. The 32-bit output is either TRUE or
                                  FALSE.
                                 */
    CM_CLOCK_GMT,               /**< Current time of adapter clock (GMT) in the
                                  format "hh:mm:ss DD/MM/YYYY".  If
                                  the clock has not been set, then
                                  "UNAVAILABLE" is returned and CM_SyncClock or
                                  CM_SetClock should be called.
                                 */
    CM_FM_STARTUP_STATUS,       /**< The error code returned by the FM startup entry point.
								  Returns "0" if the device is
                                  not FM-enabled, there are no FMs in the
                                  device, or when there is an FM, and it is
                                  disabled).
                                  This constant can be used in CM_GetInfoLong()
                                  function.
                                 */
    CM_RTC_AAC_ENABLED,         /**< The current status of the RTC Adjustment
                                  access control. "TRUE" if access control is
                                  enabled, and "FALSE" if it is disabled.
                                 */
    CM_RTC_AAC_GUARD_SECONDS,   /**< The current maximum amount of RTC
                                  adjustments (in number of seconds) setting. It
                                  is only in effect if CM_RTC_AAC_ENABLED is TRUE.
                                  This is a numerical value.
                                 */
    CM_RTC_AAC_GUARD_COUNT,     /**< The current maximum number of RTC adjustments
                                  setting. It is only in effect if
                                  CM_RTC_AAC_ENABLED is TRUE.
                                  This is a numerical value.
                                 */
    CM_RTC_AAC_GUARD_DURATION,  /**< The current gurad duration for the
                                  enforcement of RTC adjustment limits. It is
                                  only in effect if CM_RTC_AAC_ENABLED is TRUE.
                                  This is a numerical value.
                                 */
    CM_HW_EXT_INFO_STR,         /**< Extra h/w information string 
                                  comma separated list of <label>=<value>
                                  which are set at manufacturing time.
                                 */
		CM_PINPAD_DESC,							/**< pinpad dscovery. Returns
																	<port as 32bits> <name as a string>
																	*/
 		

    CM_MAX_E_INFO
} CM_eInfo;

/** Result of a CM_GetInfoLong() call for CM_FM_STATUS.  */
#define CM_FM_DISABLED   (CK_ULONG)0x00000000uL
/** Result of a CM_GetInfoLong() call for CM_FM_STATUS.  */
#define CM_FM_ENABLED    (CK_ULONG)0x00000001uL
/** Result of a CM_GetInfoLong() call for CM_FM_STATUS.  */
#define CM_NO_FM_LOADED  (CK_ULONG)0x00000002uL
/** Result of a CM_GetInfoLong() call for CM_FM_STATUS.  */
#define CM_NO_FM_SUPPORT (CK_ULONG)0x00000003uL

/**
 * @defgroup cmfuncs CM Library Functions
 * @{
 */

/**
 * Call C_Initialize(NULL) on the underlying cryptoki provider.
 *
 * @note
 *  Library clients must call this method instead of C_Initialize directly,
 *  as it also sets up internal data required by other library methods.
 */
CK_RV CM_Initialize(void);

/**
 * Call C_Finalize(NULL) on the underlying cryptoki provider.
 *
 * @note
 *  Library clients must call this method instead of C_Finalize directly,
 *  as it also tidies up internal data used by other library methods.
 */
CK_RV CM_Finalize(void);

/**
 * Retrieve information concerning a device or slot.
 *
 * @param deviceNumber
 *  device to query
 *
 * @param itemNumber
 *  depends on the value of eInfo. See CM_eInfo.
 *
 * @param eInfo
 *  enum value representing information to retrieve. See CM_eInfo.
 *
 * @param strInfo
 *  Location to hold string result. Upto *pStrLen-1 characters will be
 *  copied into this buffer. Set this to NULL to get the field size
 *  (less terminating NULL).
 *
 * @param pStrLen
 *  on invocation, *pStrLen is the total size of strInfo, on return
 *  it is the number of characters copied into strInfo, excluding the
 *  terminating NULL.
 */
CK_RV CM_GetInfo(int deviceNumber,
                 int itemNumber,
                 CM_eInfo eInfo,
                 char* strInfo,
                 int* pStrLen);

/**
 * Retrieve information concerning a device or slot.
 *
 * @param deviceNumber
 *  device to query
 *
 * @param itemNumber
 *  depends on the value of eInfo. See CM_eInfo.
 *
 * @param eInfo
 *  enum value representing information to retrieve. See CM_eInfo.
 *
 * @param pInfo
 *  Address of a 32-bit variable that will hold the output value. It must not
 *  be NULL. The maining of the returned value is determined by eInfo. Also see
 *  CM_eInfo.
 */
CK_RV CM_GetInfoLong(int deviceNumber,
                     int itemNumber,
                     CM_eInfo eInfo,
                     CK_ULONG* pInfo);

/**
 * Convert a Cryptoki slot id into a slot reference.
 *
 * @param slotId
 *  Cryptoki slot id to convert.
 *
 * @param pSlotRef
 *  location of resulting CM_SlotRef.
 */
CK_RV CM_SlotIdToSlotRef(CK_SLOT_ID slotId,
                         CM_SlotRef* pSlotRef);

/**
 * Convert a slot reference.into a Cryptoki slot id.
 *
 * @param pSlotRef
 *  Slot Reference to convert.
 *
 * @return
 *  one of :-
 *  - Corresponding Cryptoki slot id.
 *  - -1 if the slot reference is invalid
 */
CK_SLOT_ID CM_SlotRefToSlotId(const CM_SlotRef* pSlotRef);

/**
 * Change the Admin SO PIN of a device.
 *
 * @param deviceNumber
 *  device to update
 *
 * @param pSOPIN
 *  current Admin Token SO PIN
 *
 * @param pNewSOPIN
 *  new Admin Token SO PIN
 */
CK_RV CM_ChangeAdminSOPIN(int deviceNumber,
                          const unsigned char* pSOPIN,
                          const unsigned char* pNewSOPIN);

/**
 * Initialise the Admin PIN of a device.
 *
 * @param deviceNumber
 *  device to update
 *
 * @param pSOPIN
 *  current Admin Token SO PIN
 *
 * @param pAdminPIN
 *  value to set as the Admin Token USER PIN
 */
CK_RV CM_InitAdminPIN(int deviceNumber,
                      const unsigned char* pSOPIN,
                      const unsigned char* pAdminPIN);

/**
 * Change the Admin PIN of a device.
 *
 * @param deviceNumber
 *  device to update
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param pNewAdminPIN
 *  new Admin Token USER PIN
 */
CK_RV CM_ChangeAdminPIN(int deviceNumber,
                        const unsigned char* pAdminPIN,
                        const unsigned char* pNewAdminPIN);

/**
 * Synchronise the clock of the device with the host.
 *
 * @param deviceNumber
 *  device to update
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 */
CK_RV CM_SyncClock(int deviceNumber,
                   const unsigned char* pAdminPIN);

/**
 * Set the clock of the device to the specified time.
 *
 * @param deviceNumber
 *  device to update
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param pTime
 *  NULL terminated string of the format "yyyymmddhhnnss00".
 *
 * @note
 *  The passed in time string must be GMT.
 */
CK_RV CM_SetClock(int deviceNumber,
                  const unsigned char* pAdminPIN,
                  const char* pTime);

/**
 * Create the specified number of slots on the specified device.
 *
 * @param deviceNumber
 *  device to update
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param numSlots
 *  number of new slots to create.
 *
 * @note
 *  This operation can only be performed if there are no current Cryptoki sessions
 *  open, and the client is the only application using Cryptoki.
 */
CK_RV CM_CreateSlots(int deviceNumber,
                     const unsigned char* pAdminPIN,
                     int numSlots);

/**
 * Delete the specified slot on the specified device.
 *
 * @param pSlotRef
 *  reference to slot to delete
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @note
 *  This operation can only be performed if there are no current Cryptoki sessions
 *  open, and the client is the only application using Cryptoki.
 */
CK_RV CM_DeleteSlot(const CM_SlotRef* pSlotRef,
                    const unsigned char* pAdminPIN);

/**
 * Set the security mode of the device.
 *
 * @param deviceNumber
 *  device to update
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param securityMode
 *  new security mode to set
 */
CK_RV CM_SetSecurityMode(int deviceNumber,
                         const unsigned char* pAdminPIN,
                         CK_FLAGS securityMode);

/**
 * Set the transport mode of the device.
 *
 * @param deviceNumber
 *  device to update
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param transportMode
 *  new transport mode to set.
 *  - 0 - None (device will tamper if removed from host)
 *  - 1 - Single (device can be removed from host once without tampering)
 *  - 2 - Continuous (device will never tamper when removed from host)
 */
CK_RV CM_SetTransportMode(int deviceNumber,
                          const unsigned char* pAdminPIN,
                          CK_NUMERIC transportMode);

/**
 * Initialise the token.
 *
 * There is an argument that says this function should not be here, as it
 * is for initializing a user Token, but the initialization may require the
 * Admin Token USER PIN, so it belongs in this library.
 *
 * @param pSlotRef
 *  reference to slot to initialise
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param pLabel
 *  NULL terminated token label to set.
 *
 * @param pSOPIN
 *  new Token SO PIN to set.
 *
 * @note
 *  If the security mode of the slot is CKF_NO_CLEAR_PINS then the Admin Token
 *  USER PIN is required, otherwise it is ignored.
 */
CK_RV CM_InitToken(const CM_SlotRef* pSlotRef,
                   const unsigned char* pAdminPIN,
                   const unsigned char* pLabel,
                   const unsigned char* pSOPIN);

/**
 * Reset the token.
 *
 * This will clear the USER PIN and change the SO PIN and Token Label.
 *
 * There is an argument that says this function should not be in this library,
 * as it is for resetting a user Token, but it complements the CM_InitToken method,
 * so it belongs in this library.
 *
 * @param pSlotRef
 *  reference to slot to reset
 *
 * @param pSOPIN
 *  current Token SO PIN
 *
 * @param pNewSOPIN
 *  new Token SO PIN to set
 *
 * @param pNewLabel
 *  new Token Label to set
 */
CK_RV CM_ResetToken(const CM_SlotRef* pSlotRef,
                    const unsigned char* pSOPIN,
                    const unsigned char* pNewSOPIN,
                    const unsigned char* pNewLabel);

/**
 * Tamper the device.
 *
 * This will erase all stored keys and data, returning the device
 * to it's original out of the box state, i.e. uninitializes Admin Token
 * and one user slot).
 *
 * @param deviceNumber
 *  device to tamper
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @note
 *  This operation can only be performed if there are no current Cryptoki sessions
 *  open, and the client is the only application using Cryptoki.
 */
CK_RV CM_TamperAdapter(int deviceNumber,
                       const unsigned char* pAdminPIN);

/**
 * Halt the device.
 *
 * The device will no longer be visible to Cryptoki applications until e8kreset.exe is executed.
 *
 * @param deviceNumber
 *  device to halt
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @note
 *  This operation can be performed even if there are current Cryptoki sessions
 *  open, and/or other applications are using Cryptoki.
 */
CK_RV CM_HaltAdapter(int deviceNumber,
                     const unsigned char* pAdminPIN);

/**
 * Retrieve an event log instance.
 *
 * Use CM_GetInfo with CM_EVENT_LOG_COUNT to determine valid values for zero based index.
 *
 * @param deviceNumber
 *  device to retrieve the event log entry from
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param index
 *  index of the entry to retrieve. Valid values are from 0 to CM_EVENT_LOG_COUNT-1.
 *
 * @param strEntry
 *  Location to hold string result. Upto *pStrEntryLen-1 characters will be
 *  copied into this buffer. Set this to NULL to get the field size
 *  (less terminating NULL).
 *
 * @param pStrEntryLen
 *  on invocation, *pStrEntryLen is the total size of strEntry, on return
 *  it is the number of characters copied into strEntry, excluding the
 *  terminating NULL.
 */
CK_RV CM_GetEventLogEntry(int deviceNumber,
                          const unsigned char* pAdminPIN,
                          int index,
                          char* strEntry,
                          int* pStrEntryLen);

/**
 * Purge the full event log of the device.
 *
 * This function will only purge the event log if it is full. Use
 * CM_GetInfo with CM_EVENT_LOG_FULL to check.
 *
 * @param deviceNumber
 *  device to purge the event log of
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 */
CK_RV CM_PurgeEventLog(int deviceNumber,
                       const unsigned char* pAdminPIN);

/**
 * Upgrade the firmware of the device.
 *
 * @param deviceNumber
 *  device to upgrade
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param pFileName
 *  NULL terminated name of the file containing the firmware
 *
 * @param bVerifyOnly
 *  - 0 - upgrade the device using the specified file
 *  - !0 - do not upgrade the device, just validate the signature of the
 *      firmware file
 *
 * @note
 *  If the firmware upgrade procedure is interrupted, the device may be left
 *  in an unusable state. Therefore, the caller must ensure that the operation
 *  cannot be terminated by the user, before this function returns.
 *
 * @note
 *  This operation can only be performed if there are no current Cryptoki sessions
 *  open, and the client is the only application using Cryptoki.
 */
CK_RV CM_UpgradeFirmware(int deviceNumber,
                         const unsigned char* pAdminPIN,
                         const char* pFileName,
                         int bVerifyOnly);

/**
 * Download a Functionality Module to a device.
 *
 * @param deviceNumber
 *  device to download the FM to
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param pFileName
 *  NULL terminated name of the file containing the FM image
 *
 * @param pCertName
 *  NULL terminated name of the certificate kept inside the AdminToken, which
 *  will be used to validate the FM image signature.
 *
 * @param bVerifyOnly
 *  - 0 - Download the FM using the specified FM image
 *  - !0 - do not download the FM image, just validate the signature
 *
 * @note
 *  If the firmware upgrade procedure is interrupted, the device may be left
 *  in an unusable state. Therefore, the caller must ensure that the operation
 *  cannot be terminated by the user, before this function returns.
 *
 * @note
 *  This operation can only be performed if there are no current Cryptoki sessions
 *  open, and the client is the only application using Cryptoki.
 */
CK_RV CM_DownloadFm(int deviceNumber,
                    const unsigned char* pAdminPIN,
                    const char* pFileName,
                    const char* pCertName,
                    int bVerifyOnly);


/**
 * Disable the downloaded FM in the device.
 *
 * @param deviceNumber
 *  device to disable FM
 *
 * @param pAdminPin
 *  current Admin Token USER PIN
 *
 * @note This function does not check whether the FM is already disabled or
 * not. If a FM that is already disabled is disabled again, the function will
 * report success.
 */
CK_RV CM_DisableFm(int deviceNumber,
                   const char *pAdminPin);

/**
 * Force a detection of the peripheral devices.
 *
 * @param deviceNumber
 *  device to perform the rescan.
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @note
 *  This operation can only be performed if there are no current Cryptoki
 *  sessions open, and the client is the only application using Cryptoki.
 */
CK_RV CM_RescanPeripherals(int deviceNumber,
                           const unsigned char* pAdminPIN);


/**
 * Set a value within the secure config of a device for the specified domain.
 * A domain is a collection of configuration items belonging, generally, to a
 * specific application.
 *
 * @param deviceNumber
 *  device on which to update config
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 *
 * @param szConfigDomain
 *  Configuration domain to set item on
 *
 * @param szItem
 *  Name of item to set
 *
 * @param szValue
 *  value to set the item to
 */
CK_RV CM_SetConfigItem(int deviceNumber,
                       const unsigned char* pAdminPIN,
                       char* szConfigDomain,
                       char* szItem,
                       char* szValue);

/**
 * Get a value from the secure config of a device for the specified domain.
 * A domain is a collection of configuration items belonging, generally, to a 
 * specific application.
 *
 * @param deviceNumber
 *  device on which to read config
 *
 * @param szConfigDomain
 *  Configuration domain to read item from
 *
 * @param szItem
 *  Name of item to get
 *
 * @param szValue
 *  Buffer to read value into - passed in as NULL to determine the required
 *  length of buffer
 *
 * @param valueLen
 *  On completion of the function, *valueLen will contain the length of the
 *  data to be returned in szValue. If the value to be returned is larger than
 *  the value passed in by valueLen, and szValue was not passed in as NULL,
 *  CKR_DATA_LEN_RANGE is returned.
 */
CK_RV CM_GetConfigItem(int deviceNumber,
                       char* szConfigDomain,
                       char* szItem,
                       char* szValue,
                       int* valueLen);

/**
 * Set new parameters for the RTC Adjustment Access Control (AAC) for the
 * specified device. These parameters control how much of an adjustment can be
 * made to the RTC within a specified duration using the unauthenticated
 * HSMADM_AdjustTime() function.
 *
 * @param deviceNumber
 *  device to Update the RTC AAC parameters.
 *
 * @param aacEnabled
 *  @li TRUE - Enable RTC Adjustment Access Control
 *  @li FALSE - Allow unlimited unauthenticated adjustments to the RTC.
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 */
CK_RV CM_SetRtcAacEnabled(int deviceNumber,
                          CK_BBOOL aacEnabled,
                          const unsigned char* pAdminPIN);


/**
 * Set new parameters for the RTC Adjustment Access Control (AAC) for the
 * specified device. These parameters control how much of an adjustment can be
 * made to the RTC within a specified duration using the unauthenticated
 * HSMADM_AdjustTime() function.
 *
 * @param deviceNumber
 *  device to Update the RTC AAC parameters.
 *
 * @param aacMaxSeconds
 *  Total amount of deviation, in number of seconds, allowed as an adjustment to
 *  the RTC within the Guard Duration.
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 */
CK_RV CM_SetRtcAacSeconds(int deviceNumber,
                          CK_NUMERIC guardSeconds,
                          const unsigned char* pAdminPIN);

/**
 * Set new parameters for the RTC Adjustment Access Control (AAC) for the
 * specified device. These parameters control how much of an adjustment can be
 * made to the RTC within a specified duration using the unauthenticated
 * HSMADM_AdjustTime() function.
 *
 * @param deviceNumber
 *  device to Update the RTC AAC parameters.
 *
 * @param aacMaxCount
 *  Total number of adjustments that can be made to the RTC within the Guard
 *  Duration.
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 */
CK_RV CM_SetRtcAacCount(int deviceNumber,
                        CK_NUMERIC guardCount,
                        const unsigned char* pAdminPIN);

/**
 * Set new parameters for the RTC Adjustment Access Control (AAC) for the
 * specified device. These parameters control how much of an adjustment can be
 * made to the RTC within a specified duration using the unauthenticated
 * HSMADM_AdjustTime() function.
 *
 * @param deviceNumber
 *  device to Update the RTC AAC parameters.
 *
 * @param durationDays
 *  The guard duration, in number of days, used to enforce the Guard Seconds and
 *  Guard Count limits.
 *
 * @param pAdminPIN
 *  current Admin Token USER PIN
 */
CK_RV CM_SetRtcAacDuration(int deviceNumber,
                           CK_NUMERIC durationDays,
                           const unsigned char* pAdminPIN);

/**
 * @}
 */

#ifdef __cplusplus
}
#endif

#endif
