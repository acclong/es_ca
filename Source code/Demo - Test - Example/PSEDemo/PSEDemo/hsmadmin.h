#ifndef INC_HSMADMIN_H
#define INC_HSMADMIN_H

#ifdef __cplusplus
    extern "C" {        /* define a 'C' functions to prevent mangling */
#endif /* #ifdef __cplusplus */

/**
 * @mainpage HSMAdmin Exportable header 
 * @section global_hsmadmin_h Global variables, struct declaration
 * @li @ref HSMADM_TimeVal_t
 * 
 * @section export Exportable functions
 * @li @ref HSMADM_GetTimeOfDay()
 * @li @ref HSMADM_AdjustTime()
 * @li @ref HSMADM_SetRtcStatus()
 * @li @ref HSMADM_GetRtcStatus()
 * @li @ref HSMADM_GetRtcAdjustAmount()
 * @li @ref HSMADM_GetRtcAdjustCount()
 * @li @ref HSMADM_GetHsmUsageLevel()
 * 
 * @section support Other supporting files
 * @li @ref hsmadmcmd.h
 */
 
/**
 * Return codes of the HSM Administration module.
 */
typedef enum HSMADM_RV_et
{
    /** Operation was successful. */
    HSMADM_OK     = 0u,

    /** One or more of the parameters have invalid value. */
    HSMADM_BAD_PARAMETER = 1u,

    /**
     * The delta value passed to the HSMADM_AdjustTime() is too large, and will
     * not be used.
     */
    HSMADM_ADJ_TIME_LIMIT = 2u,

    /**
     * The number of calls made to the HSMADM_AdjustTime() that change the time
     * is too large. The adjustment will not be made.
     */
    HSMADM_ADJ_COUNT_LIMIT = 3u,

    /** Not enough memory to complete operation. */
    HSMADM_NO_MEMORY     = 4u,

    /** There was a system error. The operation was not performed. */
    HSMADM_SYSERR        = 5u
} HSMADM_RV;

/**
 * Possible values of the RTC status in the HSM. 
 */
typedef enum HSMADM_RtcStatus_et
{
    /** The RTC is not initialized yet. */
    HSMADM_RTC_UNINITIALIZED = 0uL,

    /** The RTC is in the stand alone mode. This means that it is completely
     * controlled by the crypto subsystem. In this mode, all cryptographic
     * operations are allowed to use the value of the clock. */
    HSMADM_RTC_STAND_ALONE = 1uL,

    /** The RTC is being controlled by an external program; but the value is not
     * trusted yet. This means certain cryptographic operations will be refused
     * because the value is (possibly) incorrect. */
    HSMADM_RTC_MANAGED_UNTRUSTED = 2uL,

    /** The RTC is being controlled by an external program, and its value may be
     * trusted. This means that all cryptographic operations are allowed to use
     * the value of the clock. */
    HSMADM_RTC_MANAGED_TRUSTED = 3uL
} HSMADM_RtcStatus_t;

/**
 * This structure is used to specify time values, or time amounts. When it is
 * used to indicate absolute time, it must be interpreted as the time passed
 * since midnight, 1 Jan 1970.
 *
 * @note Eracom's HSM real-time clock only has milli-second resolution. As 
 * such, tv_usec is always rounded-up to milli-seconds.
 */
typedef struct HSMADM_TimeVal_st
{
    /** Number of seconds. */
    long tv_sec;

    /** Number of microseconds. */
    long tv_usec;
} HSMADM_TimeVal_t;

/**
 * Obtain the current time of day from the HSM RTC.
 *
 * @param hsmIndex
 *    The zero-based index of the HSM number to be used.
 *
 * @param tv
 *    Address of the variable which will be initialized with the current
 *    time of day.
 */
HSMADM_RV HSMADM_GetTimeOfDay(unsigned int hsmIndex,
                              HSMADM_TimeVal_t *tv);

/**
 * Either adjust the time, or obtain the current adjustment value.
 * The parameter, delta, indicates the adjustment factor to be applied to the
 * HSM RTC. If there is an adjustment being performed when this function is
 * called, the remaining adjustment factor is discarded, and the new adjustment
 * value is used instead.
 * This function can also be used to obtain the remanining adjustment amount. If
 * the parameter @c delta is @c NULL, and @c oldDelta is a valid pointer, it
 * will return the current delta value.
 *
 * @param hsmIndex
 *    The zero-based index of the HSM number to be used.
 *
 * @param delta
 *    The amount of adjustment to be made to the RTC. This parameter must be @c
 *    NULL, is @c oldDelta is not @c NULL.
 *
 * @param oldDelta
 *    Address of the variable that will receive the remaining amount of
 *    adjustment from a previous call. This parameter must be @c NULL is @c
 *    delta is not @c NULL.
 *
 * @return
 *    If the adjustment value is too large, then @c HSMADM_VALUE_RANGE will be
 *    returned.
 */
HSMADM_RV HSMADM_AdjustTime(unsigned int hsmIndex,
                            const HSMADM_TimeVal_t *delta,
                            HSMADM_TimeVal_t *oldDelta);

/**
 * Change the HSM RTC status. An external manager may use this function to
 * change the status of the RTC.
 *
 * @param hsmIndex
 *    The zero-based index of the HSM number to be used.
 *
 * @param status
 *    The new status of the RTC.
 *
 * @return
 *    If the status value is invalid, then @c HSMADM_BAD_PARAMETER will be
 *    returned.
 */
HSMADM_RV HSMADM_SetRtcStatus(unsigned int hsmIndex,
                              HSMADM_RtcStatus_t status);

/**
 * Obtain the HSM RTC status. An application may use this function to determine
 * the availability or reliability of the RTC.
 *
 * @param hsmIndex
 *    The zero-based index of the HSM number to be used.
 *
 * @param status
 *    The address of the variable that will obtain the current status of the
 *    RTC. This parameter must not be @c NULL.
 *
 * @return
 *    If the status value is invalid, then @c HSMADM_BAD_PARAMETER will be
 *    returned.
 */
HSMADM_RV HSMADM_GetRtcStatus(unsigned int hsmIndex,
                              HSMADM_RtcStatus_t *status);


/**
 * Get the effective total amount, in milliseconds, of adjustments made to the
 * RTC using the HSMADM_AdjustTime() function.
 *
 * @param hsmIndex
 *    The zero-based index of the HSM number to be used. 
 *
 * @param totalMs
 *   Address of the variable that will obtain the value. This parameter must not
 *   be @c NULL. (This parameter is only valid if RTC Access Control is enabled.)
 */
HSMADM_RV HSMADM_GetRtcAdjustAmount(unsigned int hsmIndex,
                                    long *totalMs);

/**
 * Get the effective count of adjustments made to the RTC using the
 * HSMADM_AdjustTime() function.
 *
 * @param hsmIndex
 *    The zero-based index of the HSM number to be used. 
 *
 * @param totalCount
 *   Address of the variable that will obtain the value. This parameter must not
 *   be @c NULL. (This parameter is only valid if RTC Access Control is enabled.)
 */
HSMADM_RV HSMADM_GetRtcAdjustCount(unsigned int hsmIndex,
                                   unsigned long *totalCount);

/**
 * Get the usage level of the hsm as a percentage i.e. the load on the HSM.
 *
 * @param hsmIndex
 *    The zero-based index of the HSM number to be used. 
 *
 * @param value
 *   Address of the variable that will obtain the value. This parameter must not
 *   be @c NULL. 
 */
HSMADM_RV HSMADM_GetHsmUsageLevel(unsigned int hsmIndex,
                                  unsigned long *value);
#ifdef __cplusplus
}
#endif /* #ifdef __cplusplus */

#endif /* INC_HSMADMIN_H */
