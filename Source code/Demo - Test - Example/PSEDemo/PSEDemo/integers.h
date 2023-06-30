/*
 * $Id: prod/include/integers.h 1.1.2.2 2010/11/14 07:36:40EST Franklin, Brian (bfranklin) Exp  $
 * $Author: Franklin, Brian (bfranklin) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/integers.h $
 * $Revision: 1.1.2.2 $
 * $Date: 2010/11/14 07:36:40EST $
 */
#ifndef INC_INTEGERS_H
#   define INC_INTEGERS_H

/* include <sys/types.h> if the platform has it */
#if defined(HAVE_SYSTYPES_H)
#   include <sys/types.h>
#endif /* #if defined(HAVE_SYSTYPES_H) */

/* include <stdint.h> if the platform has it */
#if defined(HAVE_STDINT_H)
#   include <stdint.h>
#endif /* #if defined(HAVE_STDINT_H) */

/* include <inttypes.h> if the platform has it */
#if defined(HAVE_INTTYPES_H)
#   include <inttypes.h>
#endif /* #if defined(HAVE_INTTYPES_H) */

/* don't include these on a LINUX driver build */
#if ! defined(__KERNEL__) 
#   include <limits.h>
#   include <stddef.h>
#endif /* #if ! defined(__KERNEL__) */

/*
 * This module contains definitions for the platform-independent types
 */

#ifndef __cplusplus
#   ifndef BOOL_DEFINED
#       define BOOL_DEFINED
#	define HAVE_BOOL
#ifndef __KERNEL__
/*	The following code is used for Linux 64. JS	*/
typedef enum bool
{
     false = 0,
     true = 1
} bool;
#endif
#   endif /* ifndef BOOL_DEFINED */
#endif /* ifndef __cplusplus */

#ifndef     TRUE
#   define     TRUE                1
#endif /* #ifndef     TRUE */
#ifndef     FALSE
#   define     FALSE               !TRUE
#endif /* #ifndef     FALSE */

#if defined(HAVE_INTTYPES_H) || defined(HAVE_STDINT_H)

    /* AIX gives us these for free; so does HPUX in device driver compile */
#   if !defined(_AIX) && !(defined(_HPUX_SOURCE) && defined(_KERNEL))
        typedef int8_t int8;
        typedef int16_t int16;
        typedef int32_t int32;
        typedef int64_t int64;
#   endif /* !defined(_AIX) && !(defined(_HPUX_SOURCE) && defined(_KERNEL)) */

    typedef uint8_t uint8;
    typedef uint16_t uint16;
    typedef uint32_t uint32;
    typedef uint64_t uint64;

#elif defined(__arm)

    typedef char int8;
    typedef short int16;
    typedef int int32;
    typedef long long int64;

    typedef int8 int8_t;
    typedef int16 int16_t;
    typedef int32 int32_t;
    typedef int64 int64_t;

    typedef unsigned char uint8;
    typedef unsigned short uint16;
    typedef unsigned int uint32;
    typedef unsigned long long uint64;

    typedef uint8 uint8_t;
    typedef uint16 uint16_t;
    typedef uint32 uint32_t;
    typedef uint64 uint64_t;

#   ifndef __ssize_t
#       define __ssize_t 1
        typedef int32 ssize_t;   /* not supplied by ARM compiler */
#   endif /* #   ifndef __ssize_t */


#elif defined(_MSC_VER) && (_MSC_VER > 800)
    typedef int ssize_t;

    typedef char int8;
    typedef short int16;
    typedef int int32;
    typedef __int64 int64;

    typedef int8 int8_t;
    typedef int16 int16_t;
    typedef int32 int32_t;
    typedef int64 int64_t;

    typedef unsigned char uint8;
    typedef unsigned short uint16;
    typedef unsigned int uint32;
    typedef unsigned __int64 uint64;

    typedef uint8 uint8_t;
    typedef uint16 uint16_t;
    typedef uint32 uint32_t;
    typedef uint64 uint64_t;

#elif defined(__WATCOMC__)

    typedef char int8;
    typedef short int16;
    typedef long int32;

    /* typedef __int64 int64; */

    typedef unsigned char uint8;
    typedef unsigned short uint16;
    typedef unsigned long uint32;

    /*typedef unsigned __int64 uint64;*/
#	ifndef   _SSIZE_T_DEFINED_ 
#   ifndef __ssize_t
#   define __ssize_t 1
    typedef int32 ssize_t;   /* not supplied by compiler */
#   endif   /* __ssize_t */
#   endif   /* _SSIZE_T_DEFINED_ */

#elif defined(_MSC_VER) && (_MSC_VER <= 800)
    typedef char int8;
    typedef short int16;
    typedef long int32;
    /* NO 64 bit integer support in 16 bit MS compilers */
    /* typedef __int64 int64; */

    typedef unsigned char uint8;
    typedef unsigned short uint16;
    typedef unsigned long uint32;
    /* NO 64 bit integer support in 16 bit MS compilers */
    /* typedef unsigned __int64 uint64; */

#elif defined (__GNUC__)

    typedef char int8;
    typedef short int16;
    typedef int int32;
    typedef long long int64;

    typedef unsigned char uint8;
    typedef unsigned short uint16;
    typedef unsigned int uint32;
    typedef unsigned long long uint64;

#endif /* ! (defined(HAVE_INTTYPES_H) || defined(HAVE_STDINT_H) */

#endif /* INC_INTEGERS_H */
