/*
 * $Id: prod/include/cprovver.h 1.1.1.4.1.5 2013/07/12 12:17:35EDT Sorokine, Joseph (jsorokine) Exp  $
 * $Author: Sorokine, Joseph (jsorokine) $
 *
 * Copyright (c) 2000,2001 ERACOM Pty. Ltd.
 * All Rights Reserved - Proprietary Information of ERACOM Pty. Ltd.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/cprovver.h $
 * $Revision: 1.1.1.4.1.5 $
 * $Date: 2013/07/12 12:17:35EDT $
 */
#ifndef INC_CPROVVER_H
#define INC_CPROVVER_H

/* NOTE: The way the constants are defined determines how the string
   CPROV_VERSION_STR is formed. If we define major as 0x2 and minor as 0x001uL,
   the string becomes "0x2.00x001uL". */
#ifdef DEMO
	#define CPROV_VER_MAJOR 1
	#define CPROV_VER_MINOR 0
	#define CPROV_VER_PATCH 0
#else
	#define CPROV_VER_MAJOR 4
	#define CPROV_VER_MINOR 3
	#define CPROV_VER_PATCH 0
#endif

#define TOSTR1(X) #X
#define TOSTR(X) TOSTR1(X)

#define CPROV_VERSION_STR TOSTR(CPROV_VER_MAJOR) "." TOSTR(CPROV_VER_MINOR) "." TOSTR(CPROV_VER_PATCH)

#define CPROV_COPYRIGHT_STR "Copyright (c) Safenet, Inc. 2009-2013"
#define CPROV_COPYRIGHT_STR2 "Copyright (c) Safenet, Inc. %s-2013"

/* Global Variables */
extern unsigned int CprovVersion, CprovRelease;

#endif /* INC_CPROVVER_H */
