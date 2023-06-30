/*
 * $Id: prod/include/ctauth.h 1.2.1.1 2010/07/21 16:17:33EDT Sorokine, Joseph (jsorokine) Exp  $
 * $Author: Sorokine, Joseph (jsorokine) $
 *
 * Copyright (c) 2009 SafeNet Inc.
 * All Rights Reserved - Proprietary Information of SafeNet Inc.
 * Not to be Construed as a Published Work.
 *
 * $Source: prod/include/ctauth.h $
 * $Revision: 1.2.1.1 $
 * $Date: 2010/07/21 16:17:33EDT $
 */
 
#ifndef CTAUTH_H
#define CTAUTH_H

#include "cryptoki.h"


#ifdef __cplusplus
extern "C" {                /* define as 'C' functions to prevent mangling */
#endif

/* Creates the challenge response */
#ifdef WIN64 
__declspec(dllexport) 
#endif
CK_RV CT_Gen_AUTH_Response(CK_BYTE_PTR pPin,
                CK_ULONG ulPinLen, CK_BYTE_PTR pChallenge,
                CK_ULONG ulChallengeLen, CK_USER_TYPE userType,
                CK_BYTE_PTR pResponse, CK_ULONG_PTR pulResponse);

#ifdef __cplusplus
}
#endif

#endif

