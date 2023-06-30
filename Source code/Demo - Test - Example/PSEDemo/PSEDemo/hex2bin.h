/*
 * $Id: prod/include/hex2bin.h 1.1.2.1 2010/01/29 14:37:31EST Belem,Karim (kbelem) Exp  $
 * $Author: Belem,Karim (kbelem) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/hex2bin.h $
 * $Revision: 1.1.2.1 $
 * $Date: 2010/01/29 14:37:31EST $
 */

#ifndef HEX2BIN_INCLUDED
#define HEX2BIN_INCLUDED

#include <integers.h>

#ifdef __cplusplus
extern "C" {                /* define as 'C' functions to prevent mangling */
#endif

size_t asc2bcd(void * bcd, const char * asc, size_t maxLen);

size_t hex2bin( void * bin, const char * hex, size_t maxLen );
size_t bin2hex( char * hex, const void * bin, size_t maxLen );
size_t bin2hexM( char * hex, const void * bin, size_t maxLen, size_t lineLen );

void memdump(const char * txt, const unsigned char * buf, size_t len);

/* parity functions */
void SetOddParity( unsigned char * string, size_t count );
int isOddParity( const unsigned char * string, size_t count );

void xormem(unsigned char * x, const unsigned char * y, size_t len);

#ifdef __cplusplus
}
#endif

#endif
