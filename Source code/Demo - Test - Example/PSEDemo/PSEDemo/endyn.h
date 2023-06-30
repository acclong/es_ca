/*
 * $Id: prod/include/endyn.h 1.1.2.1 2010/01/29 14:37:32EST Belem,Karim (kbelem) Exp  $
 * $Author: Belem,Karim (kbelem) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/endyn.h $
 * $Revision: 1.1.2.1 $
 * $Date: 2010/01/29 14:37:32EST $
 */
#ifndef INC_ENDYN_H
#define INC_ENDYN_H

#include <integers.h>

#ifdef __cplusplus
extern "C" {
#endif

void BigEndianBuf(void * tgt, void * src, size_t len);

uint16			fromBEs(uint16 val);
uint16			toBEs(uint16 val);
unsigned long	fromBEl(uint32 val, const char* file, int line);
uint32			toBEl(unsigned long val, const char* file, int line);

#define hton_short(x) toBEs(x)
#define ntoh_short(x) fromBEs(x)
#define hton_long(x) toBEl(x,__FILE__,__LINE__)
#define ntoh_long(x) fromBEl(x,__FILE__,__LINE__)

#ifdef __cplusplus
}
#endif

#endif /* INC_ENDYN_H */
