/*
 * $Id: prod/include/genmacro.h 1.1.2.1 2010/01/29 14:37:27EST Belem,Karim (kbelem) Exp  $
 * $Author: Belem,Karim (kbelem) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/genmacro.h $
 * $Revision: 1.1.2.1 $
 * $Date: 2010/01/29 14:37:27EST $
 */
#ifndef INC_GENMACRO_H
#define INC_GENMACRO_H

/* This header file contains generic macros we use in our projects. */

/* This macro is used to create a static string containing the CVS Info for the
 * current file.
 *
 * Due to the code being compiled with -Wall -Werror for Linux, we need to use
 * the special gcc __attribute__ command to stop "defined but not used"
 * warnings for the fileVersion string.
 */
#if defined(linux)
#   define GCC_SPECIAL_OPTION __attribute__((unused))
#else
#   define GCC_SPECIAL_OPTION
#endif

#define FILE_VERSION(file, version) \
                static const char* fileVersion GCC_SPECIAL_OPTION \
                     = "FileVersion: " #file " " #version

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

/* This macro is used in the beginning of a function to designate the unused
   parameters. It serves two purposes: 
   i) self documenting the code: It indicates that the parameter that is not
   used is not used intentionally.
   ii) Stopping compiler warnings about unused variables.
*/
#define ARG_USED(X) ((X) = (X))

/* Determine the offset of a member in a structure. The first parameter is a
   type name (the structure), and the second parameter is a member name in the
   specified type.  
   The subtraction of (char *)0 is done to handle the machines
   where a pointer, is not the same as an integer (this should not happen,
   but...).  (We assume sizeof(char) is 1) */
#define MEMBER_OFFSET(T, E) (unsigned int)((char *)&(((T *)0)->E) - (char *)0)

/* Round-up the value X to a multiple of the value M. */
#define ROUND_UP(X, M) ((X) + (((M)-((X)%(M)))%(M)))

/* Round down the value of X to a multiple of the value of M. */
#define ROUND_DOWN(X, M) ((X) - ((X)%(M)))

/* Returns the minimum of the two values */
#ifndef MIN
#   define MIN(a,b) ( (a) < (b) ? (a) : (b) )
#endif

/* Returns the maximum of the two values */
#ifndef MAX
#   define MAX(a,b) ( (a) > (b) ? (a) : (b) )
#endif

/* Returns the absolute value of the numeric value. */
#ifndef ABS
#	define ABS(x) (((x)>0)?(x):-(x))
#endif

/* Returns the sign of the numeric value. That is, if x is 0, SGN(x) is 0, if x
 * is negative, SGN(x) is -1, and if X is positive, SGN(x) is 1. */
#define SGN(x) (((x)>0)?1:(((x)<0)?-1:0))

/* Makes bit mask definitions easier to read. */
#ifndef BIT /* for csa7000 f/w might be already defined */
#define BIT(bit)    (1uL << (bit))
#endif

/* Makes bit mask definitions easier to read. Creates a constant that can be
 * assigned to a uint32. */
#define UINT32_BIT(bit) ((uint32)1 << (bit))

/* Makes bit mask definitions easier to read. Creates a constant that can be
 * assigned to a uint16. */
#define UINT16_BIT(bit) ((uint16)1 << (bit))

/* Makes bit mask definitions easier to read. Creates a constant that can be
 * assigned to a uint8. */
#define UINT8_BIT(bit)   ((uint8)1 << (bit))

/* This macro swaps the endianness of an unsigned short value. It assumes 16 bit
   unsigned short. */
#define ENDIAN_SWP_S(X) ((((unsigned short)(X) >> 8)&0xff) | \
        (((unsigned short)(X) << 8) & 0xff00u))

/* This macro swaps the endianness of an unsigned long value. It assumes 32 bit
   unsigned long. */
#define ENDIAN_SWP_L(X) ((((X) >> 24)&0xffu) | (((X) >> 8) & 0xff00u) | \
        (((X) << 8) & 0xff0000u) | (((X) << 24) & 0xff000000u))

/* AIX returns NULL on malloc(0), calloc(0) and realloc(0) whereas we
 * implicitly assume the opposite.
 *
 * Linux returns NULL on realloc(0) whereas we implicitly assume the opposite.
 */
#ifdef _AIX
#  define SAFE_MALLOC(x) malloc(x == 0 ? 1 : x)
#  define SAFE_REALLOC(p, x) realloc(p, x == 0 ? 1 : x)
#  define SAFE_CALLOC(n, s) calloc(n == 0 ? 1 : n, s == 0 ? 1 : s)
#elif defined(linux)
#  define SAFE_MALLOC(x) malloc(x)
#  define SAFE_REALLOC(p, x) realloc(p, x == 0 ? 1 : x)
#  define SAFE_CALLOC(n, s) calloc(n, s)
#else
#  define SAFE_MALLOC(x) malloc(x)
#  define SAFE_REALLOC(p, x) realloc(p, x)
#  define SAFE_CALLOC(n, s) calloc(n, s)
#endif /* #ifdef _AIX */

#endif /* INC_GENMACRO_H */
