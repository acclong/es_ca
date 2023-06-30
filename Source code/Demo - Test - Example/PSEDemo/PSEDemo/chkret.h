/*
 * $Id: prod/cprov/util/include/chkret.h 1.1.2.1.1.1 2011/01/26 14:45:55EST Jenkins,Tim (tjenkins) Exp  $
 * $Author: Jenkins,Tim (tjenkins) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/cprov/util/include/chkret.h $
 * $Revision: 1.1.2.1.1.1 $
 * $Date: 2011/01/26 14:45:55EST $
 */
#ifndef NOIDENT
#pragma ident "@(#)RELEASE VERSION $Name: Win64 build fix  $ SAFENET"
#endif

#ifndef CHKRET_INCLUDED
#define CHKRET_INCLUDED

#define CHECK_RV(msg, rv) \
	if ( rv ) { \
		CT_ErrorString(rv,ErrorString,sizeof(ErrorString)); \
		printf("%s: error %lx, %s\n", msg, (unsigned long)rv, ErrorString); \
	}
#define CHECK_RV_RET(msg, rv) \
	if ( rv ) { \
		CHECK_RV(msg, rv); \
		return rv; \
	}

/* check for session and object leaks */
#define LeakCheck(txt) \
		rv = GetObjectCount(slotID, &objCount); \
		if ( objCount != ObjCount ) { \
			printf( "%s: Wrong object count %lu\n", \
				txt, (unsigned long)objCount ); \
			DestroyAllObjects(slotID, NULL); \
		} \
		rv = GetSessionCount(slotID, &sessionCount, &rwSessionCount); \
		if ( sessionCount != SessionCount || \
			 rwSessionCount != RwSessionCount) { \
			printf( "%s: Wrong session count ro = %lu rw = %lu\n", \
				txt, (unsigned long)sessionCount, \
				(unsigned long)rwSessionCount ); \
			C_CloseAllSessions(slotID); \
		} \
		getFreeMem(slotID, &freeMem); \
		if ( freeMem != FreeMem ) { \
			if ( iter > 0 ) \
				printf( "%s:possible token memory leak; freemem was %lu; now %lu\n", \
					txt, (unsigned long) FreeMem, (unsigned long)freeMem ); \
			FreeMem = freeMem; \
		}

#endif
