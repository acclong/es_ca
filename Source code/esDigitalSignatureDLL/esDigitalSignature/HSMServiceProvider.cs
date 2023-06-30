using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using esDigitalSignature.eToken;
using System.Data;
using Microsoft.Win32;

namespace esDigitalSignature
{
    /// <summary>
    /// Quyền đăng nhập HSM
    /// </summary>
    public enum HSMLoginRole
    {
        /// <summary>
        /// Security Officer (hoặc Administration Security Officer nếu đăng nhập AdminSlot)
        /// </summary>
        SecurityOfficer = PKCS11.CKU_SO,
        /// <summary>
        /// User (hoặc Administrator nếu đăng nhập AdminSlot)
        /// </summary>
        User = PKCS11.CKU_USER,
    }

    /// <summary>
    /// Các kết quả có thể trả về khi thực hiện hàm
    /// </summary>
    public enum HSMReturnValue
    {
        /// <summary>
        /// Thành công
        /// </summary>
        OK = PKCS11.CKR_OK,
        /// <summary>
        /// CANCEL
        /// </summary>
        CANCEL = PKCS11.CKR_CANCEL,
        /// <summary>
        /// HOST_MEMORY
        /// </summary>
        HOST_MEMORY = PKCS11.CKR_HOST_MEMORY,
        /// <summary>
        /// SLOT_ID_INVALID
        /// </summary>
        SLOT_ID_INVALID = PKCS11.CKR_SLOT_ID_INVALID,
        /// <summary>
        /// GENERAL_ERROR
        /// </summary>
        GENERAL_ERROR = PKCS11.CKR_GENERAL_ERROR,
        /// <summary>
        /// FUNCTION_FAILED
        /// </summary>
        FUNCTION_FAILED = PKCS11.CKR_FUNCTION_FAILED,
        /// <summary>
        /// ARGUMENTS_BAD
        /// </summary>
        ARGUMENTS_BAD = PKCS11.CKR_ARGUMENTS_BAD,
        /// <summary>
        /// NO_EVENT
        /// </summary>
        NO_EVENT = PKCS11.CKR_NO_EVENT,
        /// <summary>
        /// NEED_TO_CREATE_THREADS
        /// </summary>
        NEED_TO_CREATE_THREADS = PKCS11.CKR_NEED_TO_CREATE_THREADS,
        /// <summary>
        /// CANT_LOCK
        /// </summary>
        CANT_LOCK = PKCS11.CKR_CANT_LOCK,
        /// <summary>
        /// ATTRIBUTE_READ_ONLY
        /// </summary>
        ATTRIBUTE_READ_ONLY = PKCS11.CKR_ATTRIBUTE_READ_ONLY,
        /// <summary>
        /// ATTRIBUTE_SENSITIVE
        /// </summary>
        ATTRIBUTE_SENSITIVE = PKCS11.CKR_ATTRIBUTE_SENSITIVE,
        /// <summary>
        /// ATTRIBUTE_TYPE_INVALID
        /// </summary>
        ATTRIBUTE_TYPE_INVALID = PKCS11.CKR_ATTRIBUTE_TYPE_INVALID,
        /// <summary>
        /// ATTRIBUTE_VALUE_INVALID
        /// </summary>
        ATTRIBUTE_VALUE_INVALID = PKCS11.CKR_ATTRIBUTE_VALUE_INVALID,
        /// <summary>
        /// DATA_INVALID
        /// </summary>
        DATA_INVALID = PKCS11.CKR_DATA_INVALID,
        /// <summary>
        /// DATA_LEN_RANGE
        /// </summary>
        DATA_LEN_RANGE = PKCS11.CKR_DATA_LEN_RANGE,
        /// <summary>
        /// Lỗi thiết bị HSM
        /// </summary>
        DEVICE_ERROR = PKCS11.CKR_DEVICE_ERROR,
        /// <summary>
        /// Lỗi bộ nhớ thiết bị HSM
        /// </summary>
        DEVICE_MEMORY = PKCS11.CKR_DEVICE_MEMORY,
        /// <summary>
        /// DEVICE_REMOVED
        /// </summary>
        DEVICE_REMOVED = PKCS11.CKR_DEVICE_REMOVED,
        /// <summary>
        /// ENCRYPTED_DATA_INVALID
        /// </summary>
        ENCRYPTED_DATA_INVALID = PKCS11.CKR_ENCRYPTED_DATA_INVALID,
        /// <summary>
        /// ENCRYPTED_DATA_LEN_RANGE
        /// </summary>
        ENCRYPTED_DATA_LEN_RANGE = PKCS11.CKR_ENCRYPTED_DATA_LEN_RANGE,
        /// <summary>
        /// FUNCTION_CANCELED
        /// </summary>
        FUNCTION_CANCELED = PKCS11.CKR_FUNCTION_CANCELED,
        /// <summary>
        /// FUNCTION_NOT_PARALLEL
        /// </summary>
        FUNCTION_NOT_PARALLEL = PKCS11.CKR_FUNCTION_NOT_PARALLEL,
        /// <summary>
        /// FUNCTION_NOT_SUPPORTED
        /// </summary>
        FUNCTION_NOT_SUPPORTED = PKCS11.CKR_FUNCTION_NOT_SUPPORTED,
        /// <summary>
        /// KEY_HANDLE_INVALID
        /// </summary>
        KEY_HANDLE_INVALID = PKCS11.CKR_KEY_HANDLE_INVALID,
        /// <summary>
        /// KEY_SIZE_RANGE
        /// </summary>
        KEY_SIZE_RANGE = PKCS11.CKR_KEY_SIZE_RANGE,
        /// <summary>
        /// KEY_TYPE_INCONSISTENT
        /// </summary>
        KEY_TYPE_INCONSISTENT = PKCS11.CKR_KEY_TYPE_INCONSISTENT,
        /// <summary>
        /// KEY_NOT_NEEDED
        /// </summary>
        KEY_NOT_NEEDED = PKCS11.CKR_KEY_NOT_NEEDED,
        /// <summary>
        /// KEY_CHANGED
        /// </summary>
        KEY_CHANGED = PKCS11.CKR_KEY_CHANGED,
        /// <summary>
        /// KEY_NEEDED
        /// </summary>
        KEY_NEEDED = PKCS11.CKR_KEY_NEEDED,
        /// <summary>
        /// KEY_INDIGESTIBLE
        /// </summary>
        KEY_INDIGESTIBLE = PKCS11.CKR_KEY_INDIGESTIBLE,
        /// <summary>
        /// KEY_FUNCTION_NOT_PERMITTED
        /// </summary>
        KEY_FUNCTION_NOT_PERMITTED = PKCS11.CKR_KEY_FUNCTION_NOT_PERMITTED,
        /// <summary>
        /// KEY_NOT_WRAPPABLE
        /// </summary>
        KEY_NOT_WRAPPABLE = PKCS11.CKR_KEY_NOT_WRAPPABLE,
        /// <summary>
        /// KEY_UNEXTRACTABLE
        /// </summary>
        KEY_UNEXTRACTABLE = PKCS11.CKR_KEY_UNEXTRACTABLE,
        /// <summary>
        /// MECHANISM_INVALID
        /// </summary>
        MECHANISM_INVALID = PKCS11.CKR_MECHANISM_INVALID,
        /// <summary>
        /// MECHANISM_PARAM_INVALID
        /// </summary>
        MECHANISM_PARAM_INVALID = PKCS11.CKR_MECHANISM_PARAM_INVALID,
        /// <summary>
        /// OBJECT_HANDLE_INVALID
        /// </summary>
        OBJECT_HANDLE_INVALID = PKCS11.CKR_OBJECT_HANDLE_INVALID,
        /// <summary>
        /// OPERATION_ACTIVE
        /// </summary>
        OPERATION_ACTIVE = PKCS11.CKR_OPERATION_ACTIVE,
        /// <summary>
        /// OPERATION_NOT_INITIALIZED
        /// </summary>
        OPERATION_NOT_INITIALIZED = PKCS11.CKR_OPERATION_NOT_INITIALIZED,
        /// <summary>
        /// Sai mã PIN
        /// </summary>
        PIN_INCORRECT = PKCS11.CKR_PIN_INCORRECT,
        /// <summary>
        /// PIN_INVALID
        /// </summary>
        PIN_INVALID = PKCS11.CKR_PIN_INVALID,
        /// <summary>
        /// PIN_LEN_RANGE
        /// </summary>
        PIN_LEN_RANGE = PKCS11.CKR_PIN_LEN_RANGE,
        /// <summary>
        /// PIN hết hiệu lực
        /// </summary>
        PIN_EXPIRED = PKCS11.CKR_PIN_EXPIRED,
        /// <summary>
        /// PIN bị khóa do nhập sai 3 lần liên tiếp
        /// </summary>
        PIN_LOCKED = PKCS11.CKR_PIN_LOCKED,
        /// <summary>
        /// SESSION_CLOSED 
        /// </summary>
        SESSION_CLOSED = PKCS11.CKR_SESSION_CLOSED,
        /// <summary>
        /// SESSION_COUNT
        /// </summary>
        SESSION_COUNT = PKCS11.CKR_SESSION_COUNT,
        /// <summary>
        /// SESSION_HANDLE_INVALID
        /// </summary>
        SESSION_HANDLE_INVALID = PKCS11.CKR_SESSION_HANDLE_INVALID,
        /// <summary>
        /// SESSION_PARALLEL_NOT_SUPPORTED
        /// </summary>
        SESSION_PARALLEL_NOT_SUPPORTED = PKCS11.CKR_SESSION_PARALLEL_NOT_SUPPORTED,
        /// <summary>
        /// SESSION_READ_ONLY
        /// </summary>
        SESSION_READ_ONLY = PKCS11.CKR_SESSION_READ_ONLY,
        /// <summary>
        /// SESSION_EXISTS
        /// </summary>
        SESSION_EXISTS = PKCS11.CKR_SESSION_EXISTS,
        /// <summary>
        /// SESSION_READ_ONLY_EXISTS
        /// </summary>
        SESSION_READ_ONLY_EXISTS = PKCS11.CKR_SESSION_READ_ONLY_EXISTS,
        /// <summary>
        /// SESSION_READ_WRITE_SO_EXISTS
        /// </summary>
        SESSION_READ_WRITE_SO_EXISTS = PKCS11.CKR_SESSION_READ_WRITE_SO_EXISTS,
        /// <summary>
        /// SIGNATURE_INVALID
        /// </summary>
        SIGNATURE_INVALID = PKCS11.CKR_SIGNATURE_INVALID,
        /// <summary>
        /// SIGNATURE_LEN_RANGE
        /// </summary>
        SIGNATURE_LEN_RANGE = PKCS11.CKR_SIGNATURE_LEN_RANGE,
        /// <summary>
        /// TEMPLATE_INCOMPLETE
        /// </summary>
        TEMPLATE_INCOMPLETE = PKCS11.CKR_TEMPLATE_INCOMPLETE,
        /// <summary>
        /// TEMPLATE_INCONSISTENT
        /// </summary>
        TEMPLATE_INCONSISTENT = PKCS11.CKR_TEMPLATE_INCONSISTENT,
        /// <summary>
        /// TOKEN_NOT_PRESENT
        /// </summary>
        TOKEN_NOT_PRESENT = PKCS11.CKR_TOKEN_NOT_PRESENT,
        /// <summary>
        /// TOKEN_NOT_RECOGNIZED
        /// </summary>
        TOKEN_NOT_RECOGNIZED = PKCS11.CKR_TOKEN_NOT_RECOGNIZED,
        /// <summary>
        /// TOKEN_WRITE_PROTECTED
        /// </summary>
        TOKEN_WRITE_PROTECTED = PKCS11.CKR_TOKEN_WRITE_PROTECTED,
        /// <summary>
        /// UNWRAPPING_KEY_HANDLE_INVALID
        /// </summary>
        UNWRAPPING_KEY_HANDLE_INVALID = PKCS11.CKR_UNWRAPPING_KEY_HANDLE_INVALID,
        /// <summary>
        /// UNWRAPPING_KEY_SIZE_RANGE
        /// </summary>
        UNWRAPPING_KEY_SIZE_RANGE = PKCS11.CKR_UNWRAPPING_KEY_SIZE_RANGE,
        /// <summary>
        /// UNWRAPPING_KEY_TYPE_INCONSISTENT
        /// </summary>
        UNWRAPPING_KEY_TYPE_INCONSISTENT = PKCS11.CKR_UNWRAPPING_KEY_TYPE_INCONSISTENT,
        /// <summary>
        /// USER_ALREADY_LOGGED_IN
        /// </summary>
        USER_ALREADY_LOGGED_IN = PKCS11.CKR_USER_ALREADY_LOGGED_IN,
        /// <summary>
        /// USER_NOT_LOGGED_IN
        /// </summary>
        USER_NOT_LOGGED_IN = PKCS11.CKR_USER_NOT_LOGGED_IN,
        /// <summary>
        /// User PIN chưa được khởi tạo
        /// </summary>
        USER_PIN_NOT_INITIALIZED = PKCS11.CKR_USER_PIN_NOT_INITIALIZED,
        /// <summary>
        /// USER_TYPE_INVALID
        /// </summary>
        USER_TYPE_INVALID = PKCS11.CKR_USER_TYPE_INVALID,
        /// <summary>
        /// USER_ANOTHER_ALREADY_LOGGED_IN
        /// </summary>
        USER_ANOTHER_ALREADY_LOGGED_IN = PKCS11.CKR_USER_ANOTHER_ALREADY_LOGGED_IN,
        /// <summary>
        /// USER_TOO_MANY_TYPES
        /// </summary>
        USER_TOO_MANY_TYPES = PKCS11.CKR_USER_TOO_MANY_TYPES,
        /// <summary>
        /// WRAPPED_KEY_INVALID
        /// </summary>
        WRAPPED_KEY_INVALID = PKCS11.CKR_WRAPPED_KEY_INVALID,
        /// <summary>
        /// WRAPPED_KEY_LEN_RANGE
        /// </summary>
        WRAPPED_KEY_LEN_RANGE = PKCS11.CKR_WRAPPED_KEY_LEN_RANGE,
        /// <summary>
        /// WRAPPING_KEY_HANDLE_INVALID
        /// </summary>
        WRAPPING_KEY_HANDLE_INVALID = PKCS11.CKR_WRAPPING_KEY_HANDLE_INVALID,
        /// <summary>
        /// WRAPPING_KEY_SIZE_RANGE
        /// </summary>
        WRAPPING_KEY_SIZE_RANGE = PKCS11.CKR_WRAPPING_KEY_SIZE_RANGE,
        /// <summary>
        /// WRAPPING_KEY_TYPE_INCONSISTENT
        /// </summary>
        WRAPPING_KEY_TYPE_INCONSISTENT = PKCS11.CKR_WRAPPING_KEY_TYPE_INCONSISTENT,
        /// <summary>
        /// RANDOM_SEED_NOT_SUPPORTED
        /// </summary>
        RANDOM_SEED_NOT_SUPPORTED = PKCS11.CKR_RANDOM_SEED_NOT_SUPPORTED,
        /// <summary>
        /// RANDOM_NO_RNG
        /// </summary>
        RANDOM_NO_RNG = PKCS11.CKR_RANDOM_NO_RNG,
        /// <summary>
        /// DOMAIN_PARAMS_INVALID
        /// </summary>
        DOMAIN_PARAMS_INVALID = PKCS11.CKR_DOMAIN_PARAMS_INVALID,
        /// <summary>
        /// BUFFER_TOO_SMALL
        /// </summary>
        BUFFER_TOO_SMALL = PKCS11.CKR_BUFFER_TOO_SMALL,
        /// <summary>
        /// SAVED_STATE_INVALID
        /// </summary>
        SAVED_STATE_INVALID = PKCS11.CKR_SAVED_STATE_INVALID,
        /// <summary>
        /// INFORMATION_SENSITIVE
        /// </summary>
        INFORMATION_SENSITIVE = PKCS11.CKR_INFORMATION_SENSITIVE,
        /// <summary>
        /// STATE_UNSAVEABLE
        /// </summary>
        STATE_UNSAVEABLE = PKCS11.CKR_STATE_UNSAVEABLE,
        /// <summary>
        /// Cryptoki chưa khởi tạo do hàm khởi tạo HSMServiceProvider(string dll) chưa được gọi hoặc thất bại
        /// </summary>
        CRYPTOKI_NOT_INITIALIZED = PKCS11.CKR_CRYPTOKI_NOT_INITIALIZED,
        /// <summary>
        /// CRYPTOKI_ALREADY_INITIALIZED
        /// </summary>
        CRYPTOKI_ALREADY_INITIALIZED = PKCS11.CKR_CRYPTOKI_ALREADY_INITIALIZED,
        /// <summary>
        /// MUTEX_BAD
        /// </summary>
        MUTEX_BAD = PKCS11.CKR_MUTEX_BAD,
        /// <summary>
        /// MUTEX_NOT_LOCKED
        /// </summary>
        MUTEX_NOT_LOCKED = PKCS11.CKR_MUTEX_NOT_LOCKED,
    }

    /// <summary>
    /// Thuật toán để gen cặp khóa
    /// </summary>
    public enum HSMKeyPairType
    {
        /// <summary>
        /// RSA
        /// </summary>
        RSA = PKCS11.CKM_RSA_PKCS_KEY_PAIR_GEN,
        /// <summary>
        /// DSA
        /// </summary>
        DSA = PKCS11.CKM_DSA_KEY_PAIR_GEN,
    }

    /// <summary>
    /// Các loại đối tượng trong slot
    /// </summary>
    public enum HSMObjectClass
    {
        /// <summary>
        /// PUBLIC_KEY
        /// </summary>
        PUBLIC_KEY = PKCS11.CKO_PUBLIC_KEY,
        /// <summary>
        /// PRIVATE_KEY
        /// </summary>
        PRIVATE_KEY = PKCS11.CKO_PRIVATE_KEY,
        /// <summary>
        /// CERTIFICATE
        /// </summary>
        CERTIFICATE = PKCS11.CKO_CERTIFICATE,
        /// <summary>
        /// CERTIFICATE_REQUEST
        /// </summary>
        CERTIFICATE_REQUEST = PKCS11.CKO_CERTIFICATE_REQUEST
    }

    /// <summary>
    /// Lớp RSA Private Key kết nối với HSM qua KeyObj
    /// </summary>
    public class HSM_RSA : AsymmetricAlgorithm
    {
        PKCS11.Object _keyObj;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="obj"></param>
        public HSM_RSA(PKCS11.Object obj)
        {
            _keyObj = obj;
        }

        /// <summary>
        /// Object thể hiện Private Key trong HSM
        /// </summary>
        public PKCS11.Object KeyObj
        {
            get { return _keyObj; }
        }

        /// <summary>
        /// KeyType
        /// </summary>
        public int KeyType
        {
            get
            {
                return PKCS11.CKK_RSA;
            }
        }

        /// <summary>
        /// KeyExchangeAlgorithm
        /// </summary>
        public override string KeyExchangeAlgorithm
        {
            get
            {
                return "RSA-PKCS1-KeyEx";
            }
        }

        /// <summary>
        /// SignatureAlgorithm
        /// </summary>
        public override string SignatureAlgorithm
        {
            get { return "http://www.w3.org/2000/09/xmldsig#rsa-sha1"; }
        }

        /// <summary>
        /// FromXmlString
        /// </summary>
        /// <param name="xmlString"></param>
        public override void FromXmlString(String xmlString)
        {
            throw new NotSupportedException("DLL_NotSupportedMethod");
        }

        /// <summary>
        /// ToXmlString
        /// </summary>
        /// <param name="includePrivateParameters"></param>
        /// <returns></returns>
        public override String ToXmlString(bool includePrivateParameters)
        {
            throw new NotSupportedException("DLL_NotSupportedMethod");
        }
    }

    /// <summary>
    /// Lớp DSA Private Key kết nối với HSM qua KeyObj
    /// </summary>
    public class HSM_DSA : AsymmetricAlgorithm
    {
        PKCS11.Object _keyObj;

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="obj"></param>
        public HSM_DSA(PKCS11.Object obj)
        {
            _keyObj = obj;
        }

        /// <summary>
        /// Object thể hiện Private Key trong HSM
        /// </summary>
        public PKCS11.Object KeyObj
        {
            get { return _keyObj; }
        }

        /// <summary>
        /// KeyType
        /// </summary>
        public int KeyType
        {
            get
            {
                return PKCS11.CKK_DSA;
            }
        }

        /// <summary>
        /// KeyExchangeAlgorithm
        /// </summary>
        public override string KeyExchangeAlgorithm
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// SignatureAlgorithm
        /// </summary>
        public override string SignatureAlgorithm
        {
            get { return "http://www.w3.org/2000/09/xmldsig#dsa-sha1"; }
        }

        /// <summary>
        /// FromXmlString
        /// </summary>
        /// <param name="xmlString"></param>
        public override void FromXmlString(String xmlString)
        {
            throw new NotSupportedException("DLL_NotSupportedMethod");
        }

        /// <summary>
        /// ToXmlString
        /// </summary>
        /// <param name="includePrivateParameters"></param>
        /// <returns></returns>
        public override String ToXmlString(bool includePrivateParameters)
        {
            throw new NotSupportedException("DLL_NotSupportedMethod");
        }
    }

    /// <summary>
    /// Lớp cung cấp giao tiếp với HSM
    /// </summary>
    public class HSMServiceProvider : IDisposable
    {
        string _dll;
        PKCS11.Session _session;
        AsymmetricAlgorithm _privatekey;
        bool _isDisposed = false;

        /// <summary>
        /// Private Key dùng để ký
        /// </summary>
        public AsymmetricAlgorithm PrivateKey
        {
            get { return _privatekey; }
        }

        /// <summary>
        /// Đã đóng session và giải phóng DLL hay chưa.
        /// </summary>
        public bool IsDisposed { get { return _isDisposed; } }

        /// <summary>
        /// Khởi tạo do nothing
        /// </summary>
        public HSMServiceProvider()
        {
            
        }

        /// <summary>
        /// Khởi tạo kết nối HSM sử dụng thư viện Cryptographic Token Interface Standard (CRYPTOKI)
        /// </summary>
        /// <param name="dll">Tên thư viện Cryptoki</param>
        public HSMServiceProvider(string dll)
        {
            PKCS11.Finalize();
            PKCS11.Initialize(dll);
            _dll = dll;
        }

        /// <summary>
        /// Khởi tạo session mới và login.
        /// <para>Phải gọi HSMServiceProvider.Initialize() để khởi tạo kết nối HSM trước khi dùng hàm khởi tạo này 
        /// và gọi HSMServiceProvider.Finalize() sau khi kết thúc sử dụng.</para>
        /// </summary>
        /// <param name="slotSerial"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        public HSMServiceProvider(string slotSerial, HSMLoginRole role, string password)
        {
            HSMReturnValue rLogin = Login(slotSerial, role, password);
            if (rLogin != HSMReturnValue.OK)
            {
                _session.Close();

                if (rLogin == HSMReturnValue.PIN_INCORRECT)
                    throw new CryptographicException("HSM_PinIncorrect");
                if (rLogin == HSMReturnValue.PIN_LOCKED)
                    throw new CryptographicException("HSM_PinLocked");
                if (rLogin == HSMReturnValue.PIN_EXPIRED)
                    throw new CryptographicException("HSM_PinExpired");
                else
                    throw new CryptographicException("HSM_LoginFailed");
            }
        }

        /// <summary>
        /// Giải phóng kết nối HSM
        /// </summary>
        public void Dispose()
        {
            try
            {
                _session.Logout();
                _session.Close();
            }
            catch { }
            //Edited by Toantk on 20/6/2015
            //Thêm kiểm tra nếu Khởi tạo kết nối DLL tĩnh (_dll = "") thì không Kết thúc kết nối ở Dispose
            if (!String.IsNullOrEmpty(_dll))
                PKCS11.Finalize();
            _isDisposed = true;
        }

        /// <summary>
        /// Khởi tạo kết nối tĩnh đến HSM
        /// </summary>
        /// <param name="dll"></param>
        public static void Initialize(string dll)
        {
            PKCS11.Finalize();
            PKCS11.Initialize(dll);
        }

        /// <summary>
        /// Đóng kết nối đến HSM
        /// </summary>
        public static void Finalize()
        {
            PKCS11.Finalize();
        }

        #region Các hàm phục vụ ký

        /// <summary>
        /// Mở session và đăng nhập vào Slot trên HSM
        /// </summary>
        /// <param name="slotID">Slot ID</param>
        /// <param name="role">Kiểu đăng nhập (Security Officer/User)</param>
        /// <param name="password">Mật khẩu đăng nhập slot</param>
        /// <returns>OK nếu đăng nhập thành công</returns>
        /// <exception cref="T:HSM_SlotNotFound">Xảy ra khi không tìm thấy slot</exception>
        public HSMReturnValue Login(int slotID, HSMLoginRole role, string password)
        {
            //Tìm token
            PKCS11.Slot slot = FindSlotByID(slotID);

            //Khởi tạo session và đăng nhập slot
            _session = PKCS11.OpenSession(slot, PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);
            return (HSMReturnValue)_session.Login((int)role, password);
        }

        /// <summary>
        /// Mở session và đăng nhập vào Slot trên HSM
        /// </summary>
        /// <param name="slotSerial">Số serial của slot</param>
        /// <param name="role">Kiểu đăng nhập (Security Officer/User)</param>
        /// <param name="password">Mật khẩu đăng nhập slot</param>
        /// <returns>OK nếu đăng nhập thành công</returns>
        /// <exception cref="T:HSM_SlotNotFound">Xảy ra khi không tìm thấy slot</exception>
        public HSMReturnValue Login(string slotSerial, HSMLoginRole role, string password)
        {
            //Tìm token
            PKCS11.Slot slot = FindSlotBySerial(slotSerial);

            //Khởi tạo session và đăng nhập slot
            _session = PKCS11.OpenSession(slot, PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);
            return (HSMReturnValue)_session.Login((int)role, password);
        }

        /// <summary>
        /// Đăng xuất và đóng session
        /// </summary>
        public void Logout()
        {
            _session.Logout();
            _session.Close();
        }

        /// <summary>
        /// Lấy certificate từ HSM theo label
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public X509Certificate2 GetCertificate(string label)
        {
            //Tìm Certificate Object
            PKCS11.Object[] certificates = _session.FindObjects(new PKCS11.Attribute[]  {
                new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_CERTIFICATE),
                new PKCS11.Attribute(PKCS11.CKA_LABEL, label)
              });

            //Get value và convert
            if (certificates.Count() > 0)
            {
                if (certificates.Count() > 1)
                    throw new CryptographicException("HSM_DuplicateCertificate");

                byte[] value = (byte[])certificates[0].Get(_session, PKCS11.CKA_VALUE);
                return new X509Certificate2(value);
            }
            else
                throw new CryptographicException("HSM_CannotLocateCertificate");
        }

        /// <summary>
        /// Lấy Private Key từ HSM theo thuộc tính LABEL
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        /// <exception cref="T:HSM_DuplicateKey"></exception>
        /// <exception cref="T:HSM_KeyTypeNotSupported"></exception>
        /// <exception cref="T:HSM_KeyNotFound"></exception>
        public AsymmetricAlgorithm LoadPrivateKeyByLABEL(string label)
        {
            //Tìm Key Object
            PKCS11.Object[] PIkeys = _session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PRIVATE_KEY),
                    new PKCS11.Attribute(PKCS11.CKA_LABEL, label)
                  });

            if (PIkeys.Count() > 0)
            {
                if (PIkeys.Count() > 1)
                    throw new CryptographicException("HSM_DuplicateKey");

                //Lấy loại key
                int keytype = (int)PIkeys[0].Get(_session, PKCS11.CKA_KEY_TYPE);
                //Khởi tạo class AsymmetricAlgorithm tương ứng
                if (keytype == PKCS11.CKK_RSA)
                {
                    _privatekey = new HSM_RSA(PIkeys[0]);
                    return _privatekey;
                }
                else if (keytype == PKCS11.CKK_DSA)
                {
                    _privatekey = new HSM_DSA(PIkeys[0]);
                    return _privatekey;
                }
                else
                    throw new CryptographicException("HSM_KeyTypeNotSupported");
            }
            else
                throw new CryptographicException("HSM_KeyNotFound");
        }

        /// <summary>
        /// Lấy Private Key từ HSM theo thuộc tính ID (một bộ PublicKey + PrivateKey + Certificate tương ứng có cùng ID)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="T:HSM_DuplicateKey"></exception>
        /// <exception cref="T:HSM_KeyTypeNotSupported"></exception>
        /// <exception cref="T:HSM_KeyNotFound"></exception>
        public AsymmetricAlgorithm LoadPrivateKeyByID(byte[] id)
        {
            //Tìm Key Object
            PKCS11.Object[] PIkeys = _session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PRIVATE_KEY),
                    new PKCS11.Attribute(PKCS11.CKA_ID, id)
                  });

            if (PIkeys.Count() > 0)
            {
                if (PIkeys.Count() > 1)
                    throw new CryptographicException("HSM_DuplicateKey");

                //Lấy loại key
                int keytype = (int)PIkeys[0].Get(_session, PKCS11.CKA_KEY_TYPE);
                //Khởi tạo class AsymmetricAlgorithm tương ứng
                if (keytype == PKCS11.CKK_RSA)
                {
                    _privatekey = new HSM_RSA(PIkeys[0]);
                    return _privatekey;
                }
                else if (keytype == PKCS11.CKK_DSA)
                {
                    _privatekey = new HSM_DSA(PIkeys[0]);
                    return _privatekey;
                }
                else
                    throw new CryptographicException("HSM_KeyTypeNotSupported");
            }
            else
                throw new CryptographicException("HSM_KeyNotFound");
        }

        /// <summary>
        /// Lấy private key theo certificate tương ứng trong HSM
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        /// <exception cref="T:HSM_DuplicateCertificate"></exception>
        /// <exception cref="T:HSM_CannotLocateCertificate"></exception>
        /// <exception cref="T:HSM_DuplicateKey"></exception>
        /// <exception cref="T:HSM_KeyTypeNotSupported"></exception>
        /// <exception cref="T:HSM_KeyNotFound"></exception>
        public AsymmetricAlgorithm LoadPrivateKeyByCertificate(X509Certificate2 certificate)
        {
            //Tìm Certificate Object theo SerialNumber
            PKCS11.Object[] certificates = _session.FindObjects(new PKCS11.Attribute[]  {
                new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_CERTIFICATE),
                new PKCS11.Attribute(PKCS11.CKA_VALUE, certificate.RawData)
              });

            if (certificates.Count() > 0)
            {
                if (certificates.Count() > 1)
                    throw new CryptographicException("HSM_DuplicateCertificate");

                //Lấy ID của Certificate Object
                byte[] id = (byte[])certificates[0].Get(_session, PKCS11.CKA_ID);
                //Tìm Key Object theo ID
                PKCS11.Object[] PIkeys = _session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PRIVATE_KEY),
                    new PKCS11.Attribute(PKCS11.CKA_ID, id)
                  });

                if (PIkeys.Count() > 0)
                {
                    if (PIkeys.Count() > 1)
                        throw new CryptographicException("HSM_DuplicateKey");

                    //Lấy loại key
                    int keytype = (int)PIkeys[0].Get(_session, PKCS11.CKA_KEY_TYPE);
                    //Khởi tạo class AsymmetricAlgorithm tương ứng
                    if (keytype == PKCS11.CKK_RSA)
                    {
                        _privatekey = new HSM_RSA(PIkeys[0]);
                        return _privatekey;
                    }
                    else if (keytype == PKCS11.CKK_DSA)
                    {
                        _privatekey = new HSM_DSA(PIkeys[0]);
                        return _privatekey;
                    }
                    else
                        throw new CryptographicException("HSM_KeyTypeNotSupported");
                }
                else
                    throw new CryptographicException("HSM_KeyNotFound");
            }
            else
                throw new CryptographicException("HSM_CannotLocateCertificate");
        }

        /// <summary>
        /// Kí chuỗi hash với private key đã khởi tạo (EMSA-PKCS-v1.5, PKCS #1 v2.2 RSA Cryptography Standart)
        /// </summary>
        /// <param name="hashvalue">Hash Value from file data</param>
        /// <param name="oid">Hash Algorithm Identifier</param>
        /// <returns>Chuỗi chữ ký</returns>
        /// <exception cref="T:HSM_LoadKeyFailed"></exception>
        /// <exception cref="T:HSM_KeyTypeNotSupported"></exception>
        public byte[] SignHash(byte[] hashvalue, string oid)
        {
            //Constant
            byte[] sha1AlgoID = PKCS11.DigestAlgoID[oid];

            //Tạo DigestInfo (see EMSA-PKCS-v1.5, PKCS #1 v2.2 RSA Cryptography Standart)
            byte[] digestInfo = new byte[sha1AlgoID.Length + hashvalue.Length];
            sha1AlgoID.CopyTo(digestInfo, 0);
            hashvalue.CopyTo(digestInfo, sha1AlgoID.Length);

            //Kí chuỗi DigestInfo bằng CKM_RSA_PKCS
            return SignData(digestInfo);
        }

        /// <summary>
        /// Kí chuỗi với private key đã khởi tạo
        /// </summary>
        /// <param name="dataToSign">Dữ liệu để ký</param>
        /// <returns>Chuỗi chữ ký</returns>
        /// <exception cref="T:HSM_LoadKeyFailed"></exception>
        /// <exception cref="T:HSM_KeyTypeNotSupported"></exception>
        public byte[] SignData(byte[] dataToSign)
        {
            if (_privatekey == null)
                throw new CryptographicException("HSM_LoadKeyFailed");

            if (_privatekey is esDigitalSignature.HSM_DSA)
            {
                PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_DSA, null);
                return _session.Sign(signMech, ((HSM_DSA)_privatekey).KeyObj, dataToSign);
            }
            else if (_privatekey is esDigitalSignature.HSM_RSA)
            {
                PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_RSA_PKCS, null);
                return _session.Sign(signMech, ((HSM_RSA)_privatekey).KeyObj, dataToSign);
            }
            else
            {
                throw new CryptographicException("HSM_KeyTypeNotSupported");
            }
        }

        /// <summary>
        /// Giải mã chuỗi bằng Private Key
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public byte[] DecryptMessage(byte[] message)
        {
            if (_privatekey == null)
                throw new CryptographicException("HSM_LoadKeyFailed");

            if (_privatekey is esDigitalSignature.HSM_RSA)
            {
                PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_RSA_PKCS, null);
                return _session.Decrypt(signMech, ((HSM_RSA)_privatekey).KeyObj, message);
            }
            else
            {
                throw new CryptographicException("HSM_KeyTypeNotSupported");
            }
        }
        #endregion

        #region Các hàm điều khiển dưới quyền Admin
        /// <summary>
        /// Lấy danh sách thiết bị HSM
        /// </summary>
        /// <returns></returns>
        public DataTable GetDeviceList()
        {
            DataTable dt = new DataTable("DeviceList");
            DataColumn dc = new DataColumn("DeviceID", typeof(int));
            dt.Columns.Add(dc);
            dc = new DataColumn("Label", typeof(string));
            dt.Columns.Add(dc); 
            dc = new DataColumn("SerialNumber", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("Manufacturer", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("Model", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("TotalMemory", typeof(int));
            dt.Columns.Add(dc);
            dc = new DataColumn("FreeMemory", typeof(int));
            dt.Columns.Add(dc);

            PKCS11.Slot[] device = GetAdminSlotList();
            for (int i = 0; i < device.Length; i++)
            {
                PKCS11.TokenInfo info = device[i].GetTokenInfo();

                DataRow dr = dt.NewRow();
                dr["DeviceID"] = i;
                dr["Label"] = info.label;
                dr["SerialNumber"] = info.serialNumber;
                dr["Manufacturer"] = info.manufacturerID;
                dr["Model"] = info.model;
                dr["TotalMemory"] = info.ulTotalPublicMemory;
                dr["FreeMemory"] = info.ulFreePublicMemory;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// Lấy danh sách slot/WLDSlot khả dụng
        /// </summary>
        /// <returns></returns>
        public DataTable GetSlotList()
        {
            DataTable dt = new DataTable("SlotList");
            DataColumn dc = new DataColumn("SlotIndex", typeof(int));
            dt.Columns.Add(dc);
            dc = new DataColumn("Label", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("SerialNumber", typeof(string));
            dt.Columns.Add(dc);
            dc = new DataColumn("TokenInitialised", typeof(bool));
            dt.Columns.Add(dc);
            dc = new DataColumn("ObjectCount", typeof(int));
            dt.Columns.Add(dc);

            PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
            for (int i = 0; i < slots.Length; i++)
            {
                PKCS11.TokenInfo info = slots[i].GetTokenInfo();
                PKCS11.Session session = PKCS11.OpenSession(slots[i], PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);
                PKCS11.Object[] objects = session.FindObjects(new PKCS11.Attribute[] { });
                session.Close();

                DataRow dr = dt.NewRow();
                dr["SlotIndex"] = slots[i].Id;
                dr["Label"] = info.label;
                dr["SerialNumber"] = info.serialNumber;
                dr["TokenInitialised"] = (info.flags & PKCS11.CKF_TOKEN_INITIALIZED) != 0? true : false;
                dr["ObjectCount"] = objects.Length;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// Đăng nhập quyền Administrator của thiết bị.
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="role"></param>
        /// <param name="pin"></param>
        /// <returns>OK nếu đăng nhập thành công</returns>
        /// <exception cref="T:HSM_AdminSlotNotFound">Xảy ra khi không tìm thấy Admin Slot của thiết bị</exception>
        public HSMReturnValue LoginAdmin(int deviceID, HSMLoginRole role, string pin)
        {
            //Tìm Admin token
            PKCS11.Slot adminSlot = FindAdminSlot(deviceID);

            //Tạo session và đăng nhập
            _session = PKCS11.OpenSession(adminSlot, PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);
            return (HSMReturnValue)_session.Login((int)role, pin);
        }

        /// <summary>
        /// Đổi mật khẩu Administrator của HSM (mật khẩu Admin Slot).
        /// <para>Chú ý: phải LoginAdmin trước khi gọi hàm này.</para>
        /// <para>Chú ý: Đăng nhập bằng role nào sẽ đổi mật khẩu của role tương ứng.</para>
        /// </summary>
        /// <param name="oldPin">Mật khẩu cũ</param>
        /// <param name="newPin">Mật khẩu mới</param>
        /// <returns></returns>
        public HSMReturnValue ChangeAdminPIN(string oldPin, string newPin)
        {
            //Set PIN
            return (HSMReturnValue)_session.SetPIN(oldPin, newPin);
        }

        /// <summary>
        /// Tạo một slot mới.
        /// <para>Chú ý: phải LoginAdmin với role User vào device tương ứng trước khi gọi hàm này.</para>
        /// <para>Chú ý: hàm này không khởi tạo token do đó sau khi thực hiện phải Dispose() và đăng nhập lại để khởi tạo.</para>
        /// </summary>
        /// <param name="slotLabel">Tên slot mới</param>
        /// <returns>Số serial của slot mới được tạo</returns>
        public string CreateSlot(string slotLabel)
        {
            //Tạo 1 slot mới
            PKCS11.Attribute[] slotAtt = new PKCS11.Attribute[] {
                new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_SLOT),
                new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                new PKCS11.Attribute(PKCS11.CKA_LABEL, slotLabel)
            };
            PKCS11.Object obj = PKCS11.Object.Create(_session, slotAtt);

            //Lấy slot serial mới được tạo
            //Toantk 4/5/2015: Sửa trả về serial của slot mới
            byte[] serialBytes = (byte[])obj.Get(_session, PKCS11.CKA_SERIAL_NUMBER);
            string serial = Encoding.UTF8.GetString(serialBytes);
            //Toantk 23/8/2015: Ghép với HSM serial
            string hsmSerial = _session.GetInfo().slot.GetTokenInfo().serialNumber;
            serial = hsmSerial + ":" + serial;

            return serial;
        }

        /// <summary>
        /// Khởi tạo token cho slot.
        /// <para>Chú ý: phải LoginAdmin với role User trước khi gọi hàm này.</para>
        /// <para>Chú ý: sau khi chạy hàm này sẽ mất session do đó phải đăng nhập lại nếu muốn tiếp tục sử dụng.</para> 
        /// </summary>
        /// <param name="slotSerial">Id của slot mới được tạo</param>
        /// <param name="slotLabel">Tên slot mới</param>
        /// <param name="slotSOPin">Mật khẩu quyền SO của slot mới</param>
        /// <param name="slotUserPin">Mật khẩu quyền User của slot mới</param>
        /// <exception cref="T:HSM_SlotNotFound">Xảy ra khi không tìm thấy slot</exception>
        public void InitToken(string slotSerial, string slotLabel, string slotSOPin, string slotUserPin)
        {
            //--Tìm slot
            //Edited by Toantk on 4/5/2015
            //Sửa tìm slot theo serial
            PKCS11.Slot newSlot = FindSlotBySerial(slotSerial);
            //--Khởi tạo token
            newSlot.InitToken(slotSOPin, slotLabel);

            //Khởi tạo User PIN
            //--Đăng xuất session của Admin slot
            _session.Logout();
            _session.Close();
            //--Đăng nhập session của Slot mới
            _session = PKCS11.OpenSession(newSlot, PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);
            int rv = _session.Login(PKCS11.CKU_SO, slotSOPin);
            PKCS11.Exception.check(rv);
            //--Khởi tạo User PIN
            _session.InitPIN(slotUserPin);

            //--Đăng xuất
            _session.Logout();
            _session.Close();
        }

        /// <summary>
        /// Xóa slot.
        /// <para>Chú ý: chỉ cho phép xóa khi slot không chứa object nào.</para>
        /// <para>Chú ý: phải đăng nhập Admin User trước khi gọi hàm này.</para>
        /// </summary>
        /// <param name="slotSerial">Số serial của slot cần xóa</param>
        /// <exception cref="T:HSM_SlotNotFound">Xảy ra khi không tìm thấy slot.</exception>
        /// <exception cref="T:HSM_SlotNotEmpty">Xảy ra khi slot đang chứa object.</exception>
        public void DestroySlot(string slotSerial)
        {
            //Kiểm tra slot có rỗng hay không
            //Toantk 4/5/2015: Sửa tìm slot theo serial
            //Toantk 18/12/2015: Bỏ check slot rỗng
            ////PKCS11.Slot slot = FindSlotBySerial(slotSerial);
            ////PKCS11.Session session = PKCS11.OpenSession(slot, PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);
            ////PKCS11.Object[] objects = session.FindObjects(new PKCS11.Attribute[] { });
            ////if (objects.Count() > 0)
            ////    throw new Exception("HSM_SlotNotEmpty");
            ////session.Close();

            //Tách lấy serial sau dấu ":"
            string shortSerial = slotSerial;
            if (slotSerial.Contains(':'))
                shortSerial = slotSerial.Split(':')[1];

            //xóa slot
            PKCS11.Object[] slots = _session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_SLOT),
                    new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                    new PKCS11.Attribute(PKCS11.CKA_SERIAL_NUMBER, shortSerial)
                  });
            if (slots.Count() > 0)
                slots[0].Destroy(_session);
            else
                throw new Exception("HSM_SlotNotFound");
        }

        /// <summary>
        /// Nhân bản token/slot. 
        /// <para>Chú ý: phải gọi hàm Finalize() sau khi hoàn thành.</para>
        /// </summary>
        /// <param name="srcSlotSerial">Số serial của slot nguồn</param>
        /// <param name="destSlotSerial">Số serial của slot đích</param>
        /// <param name="userPin">User PIN</param>
        /// <param name="destHsmSerial">Số serial của HSM đích (HSM Identity Key's ID)</param>
        public void ReplicateToken(string srcSlotSerial, string destSlotSerial, string userPin, string destHsmSerial)
        {
            try
            {
                //Đăng nhập source token và wrap
                this.Login(srcSlotSerial, HSMLoginRole.User, userPin);
                byte[] wrappedData = PKCS11.WrapToken(_session, destHsmSerial);
                this.Logout();

                //Đóng tất cả session của destination token
                PKCS11.Slot slot = FindSlotBySerial(destSlotSerial);
                slot.CloseAllSessions();

                //Đăng nhập destination token và unwrap
                this.Login(destSlotSerial, HSMLoginRole.User, userPin);
                PKCS11.UnwrapToken(_session, wrappedData);
                this.Logout();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public byte[] Wrap(string srcSlotSerial, string userPin, string destHsmSerial)
        {
            try
            {
                //Đăng nhập source token và wrap
                this.Login(srcSlotSerial, HSMLoginRole.User, userPin);
                byte[] wrappedData = PKCS11.WrapToken(_session, destHsmSerial);
                this.Logout();

                return wrappedData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Unwrap(string destSlotSerial, string userPin, byte[] wrappedData)
        {
            //Đóng tất cả session của destination token
            PKCS11.Slot slot = FindSlotBySerial(destSlotSerial);
            slot.CloseAllSessions();

            //Đăng nhập destination token và unwrap
            this.Login(destSlotSerial, HSMLoginRole.User, userPin);
            PKCS11.UnwrapToken(_session, wrappedData);
            this.Logout();

            return "";
        }
        #endregion

        #region Các hàm điều khiển dưới quyền slot
        /// <summary>
        /// Đổi mật khẩu slot.
        /// <para>Chú ý: phải Login() trước khi gọi hàm này.</para>
        /// <para>Chú ý: Đăng nhập bằng role nào sẽ đổi mật khẩu của role tương ứng.</para>
        /// </summary>
        /// <param name="oldPin">Mật khẩu cũ</param>
        /// <param name="newPin">Mật khẩu mới</param>
        /// <returns></returns>
        public HSMReturnValue ChangeSlotPIN(string oldPin, string newPin)
        {
            //Set PIN
            return (HSMReturnValue)_session.SetPIN(oldPin, newPin);
        }

        /// <summary>
        /// Hàm tạo cặp khóa Private key + Public key và tạo certificate request.
        /// </summary>
        /// <param name="keyType">Kiểu cặp khóa: RSA/DSA</param>
        /// <param name="subject">Tên CN của chứng thư số</param>
        /// <param name="labelPUB">Tên public key trong HSM</param>
        /// <param name="labelPRV">Tên private key trong HSM</param>
        /// <param name="labelREQ">Tên request trong HSM</param>
        /// <returns>ID cặp khóa</returns>
        public byte[] GenerateKeyPairAndRequest(HSMKeyPairType keyType, string subject, string labelPUB, string labelPRV, string labelREQ)
        {
            //Loại khóa
            PKCS11.Mechanism keyMech = new PKCS11.Mechanism((int)keyType, null);

            //Thuộc tính Public Key và Private Key
            PKCS11.Attribute[] publicKeyAtt = new PKCS11.Attribute[] {
                new PKCS11.Attribute(PKCS11.CKA_SUBJECT, subject), 
                new PKCS11.Attribute(PKCS11.CKA_LABEL, labelPUB),
                new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                new PKCS11.Attribute(PKCS11.CKA_PRIME_BITS, 1024),
                new PKCS11.Attribute(PKCS11.CKA_PRIVATE, false),
                new PKCS11.Attribute(PKCS11.CKA_MODIFIABLE, true),
                new PKCS11.Attribute(PKCS11.CKA_ENCRYPT, true),
                new PKCS11.Attribute(PKCS11.CKA_VERIFY, true),
                new PKCS11.Attribute(PKCS11.CKA_DERIVE, true),
                new PKCS11.Attribute(PKCS11.CKA_WRAP, true)
            };

            PKCS11.Attribute[] privateKeyAtt = new PKCS11.Attribute[] {
                new PKCS11.Attribute(PKCS11.CKA_SUBJECT, subject), 
                new PKCS11.Attribute(PKCS11.CKA_LABEL, labelPRV),
                new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                new PKCS11.Attribute(PKCS11.CKA_PRIVATE, true),
                new PKCS11.Attribute(PKCS11.CKA_MODIFIABLE, true),
                new PKCS11.Attribute(PKCS11.CKA_SENSITIVE, true),
                new PKCS11.Attribute(PKCS11.CKA_EXTRACTABLE, true),
                new PKCS11.Attribute(PKCS11.CKA_EXPORTABLE, true),
                new PKCS11.Attribute(PKCS11.CKA_DECRYPT, true),
                new PKCS11.Attribute(PKCS11.CKA_SIGN, true),
                new PKCS11.Attribute(PKCS11.CKA_DERIVE, true),
                new PKCS11.Attribute(PKCS11.CKA_UNWRAP, true),
                new PKCS11.Attribute(PKCS11.CKA_IMPORT, false),
                new PKCS11.Attribute(PKCS11.CKA_SIGN_LOCAL_CERT, true)
            };

            //Tạo cặp key
            PKCS11.Object pubKey;
            PKCS11.Object prvKey;
            PKCS11.GenerateKeyPair(_session, keyMech, publicKeyAtt, out pubKey, privateKeyAtt, out prvKey);

            //Tạo certification request
            PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_SHA1_RSA_PKCS, null);
            _session.SignInit(signMech, prvKey);

            PKCS11.Mechanism reqMech = new PKCS11.Mechanism(PKCS11.CKM_ENCODE_PKCS_10, null);
            PKCS11.Attribute[] requestAtt = new PKCS11.Attribute[] {
                new PKCS11.Attribute(PKCS11.CKA_LABEL, labelREQ),
                new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                new PKCS11.Attribute(PKCS11.CKA_PRIVATE, false),
                new PKCS11.Attribute(PKCS11.CKA_MODIFIABLE, true),
                new PKCS11.Attribute(PKCS11.CKA_DERIVE, true)
            };
            pubKey.DeriveKey(_session, reqMech, requestAtt);

            //Trả về ID của cặp key
            return (byte[])pubKey.Get(_session, PKCS11.CKA_ID);
        }

        /// <summary>
        /// Hàm xuất certificate request ra chuỗi text định dạng PEM.
        /// <para>Chú ý: ghi chuỗi này vào file và gửi cho nhà cung cấp CA để đăng ký chứng thư số.</para>
        /// </summary>
        /// <param name="reqID">Trường ID của request trong HSM</param>
        /// <returns></returns>
        /// <exception cref="T:HSM_CertificateRequestNotFound">nếu không tìm thấy Certificate Request trong HSM</exception>
        public string ExportCertificateRequestToPEM(byte[] reqID)
        {
            string reqBase64 = "";

            PKCS11.Object[] reqs = _session.FindObjects(new PKCS11.Attribute[]  {
                new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_CERTIFICATE_REQUEST),
                new PKCS11.Attribute(PKCS11.CKA_ID, reqID)
            });

            if (reqs.Count() > 0)
            {
                byte[] val = (byte[])reqs[0].Get(_session, PKCS11.CKA_VALUE);

                reqBase64 = "-----BEGIN CERTIFICATE REQUEST-----\n"
                    + Convert.ToBase64String(val, Base64FormattingOptions.InsertLineBreaks)
                    + "\n-----END CERTIFICATE REQUEST-----";
            }
            else
                throw new Exception("HSM_CertificateRequestNotFound");

            return reqBase64;
        }

        /// <summary>
        /// Import certificate vào HSM từ X509Certificate. Trả về CKA_ID của object
        /// </summary>
        /// <param name="keyID">ID của public key tương ứng trong HSM</param>
        /// <param name="labelCERT">Tên đối tượng certificate trong HSM</param>
        /// <param name="certificate">Chứng thư số định dạng X509Certificate được đọc từ file PEM</param>
        /// <exception cref="T:HSM_PublicKeyNotMatch">Không tìm thấy public key tương ứng trong slot</exception>
        public byte[] ImportCertificateFromX509(string labelCERT, X509Certificate2 certificate)
        {
            //Lấy public key từ cert và trừ đi 7 byte đầu và 5 byte cuối định danh của nhà cung cấp
            List<byte> cert_pub = new List<byte>(certificate.GetPublicKey());
            if (cert_pub.Count > 128)
            {
                cert_pub.RemoveRange(0, 7);
                cert_pub.RemoveRange(128, 5);
            }
            //Lấy public key trong HSM để kiểm tra có khớp hay không
            PKCS11.Object[] keys = _session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PUBLIC_KEY),
                    new PKCS11.Attribute(PKCS11.CKA_MODULUS, cert_pub.ToArray())
                  });
            if (keys.Count() <= 0)
                throw new Exception("HSM_PublicKeyNotMatch");

            //Toantk 23/12/2015: Lấy CKA_ID từ public key object thay cho truyền tham số hàm
            //Lấy CKA_ID
            byte[] keyID = (byte[])keys[0].Get(_session, PKCS11.CKA_ID);

            //Thuộc tính của Certificate
            PKCS11.Attribute[] certAtt = new PKCS11.Attribute[] { 
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_CERTIFICATE),
                    new PKCS11.Attribute(PKCS11.CKA_LABEL, labelCERT),
                    new PKCS11.Attribute(PKCS11.CKA_VALUE, certificate.RawData),
                    new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                    new PKCS11.Attribute(PKCS11.CKA_CERTIFICATE_TYPE, 0),
                    new PKCS11.Attribute(PKCS11.CKA_KEY_TYPE, PKCS11.CKK_RSA),
                    new PKCS11.Attribute(PKCS11.CKA_ID, keyID)
                };

            PKCS11.Object obj = PKCS11.Object.Create(_session, certAtt);

            return keyID;
        }

        /// <summary>
        /// Thiết lập trạng thái hiệu lực hoặc không hiệu lực cho đối tượng trong slot.
        /// </summary>
        /// <param name="label">Tên đối tượng</param>
        /// <param name="objClass">Loại đối tượng</param>
        /// <param name="objID">ID đối tượng</param>
        /// <param name="isEnable">Trạng thái thiết lập</param>
        public void SetObjectStatus(string label, HSMObjectClass objClass, byte[] objID, bool isEnable)
        {
            //Lấy object
            PKCS11.Object[] objs = _session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_LABEL, label),
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, (int)objClass),
                    new PKCS11.Attribute(PKCS11.CKA_ID, objID)
                  });

            foreach (PKCS11.Object obj in objs)
            {
                if (objClass == HSMObjectClass.PUBLIC_KEY)
                    obj.Set(_session, new PKCS11.Attribute[] { 
                        new PKCS11.Attribute(PKCS11.CKA_ENCRYPT, isEnable),
                        new PKCS11.Attribute(PKCS11.CKA_VERIFY, isEnable)
                    });
                else if (objClass == HSMObjectClass.PRIVATE_KEY)
                    obj.Set(_session, new PKCS11.Attribute[] { 
                        new PKCS11.Attribute(PKCS11.CKA_DECRYPT, isEnable),
                        new PKCS11.Attribute(PKCS11.CKA_SIGN, isEnable)
                    });
                else if (objClass == HSMObjectClass.CERTIFICATE)
                    obj.Set(_session, new PKCS11.Attribute[] {
                        new PKCS11.Attribute(PKCS11.CKA_VERIFY, isEnable)
                    });
            }
        }

        /// <summary>
        /// Xóa các đối tượng trong slot.
        /// </summary>
        /// <param name="label">Tên đối tượng</param>
        /// <param name="objClass">Loại đối tượng</param>
        /// <param name="objID">ID đối tượng</param>
        public void DeleteObject(string label, HSMObjectClass objClass, byte[] objID)
        {
            //Lấy object
            PKCS11.Object[] objs = _session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_LABEL, label),
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, (int)objClass),
                    new PKCS11.Attribute(PKCS11.CKA_ID, objID)
                  });

            foreach (PKCS11.Object obj in objs)
            {
                obj.Destroy(_session);
            }
        }
        #endregion

        #region Các hàm điều khiển WLD Slot
        /// <summary>
        /// Tạo WLD slot trong Registry mới để trỏ đến HSM Slot cùng token label.
        /// </summary>
        /// <param name="wldSlotID">Chỉ số slot, sẽ được được cấu hình trong Registry dạng tên khóa ET_PKCS_WLD_SLOT_[wldSlotID]</param>
        /// <param name="tokenLabel">Token label để ánh xạ đến các slot vật lý cùng tên, sẽ được lưu trong Registry</param>
        /// <param name="serial">Số Serial tự đặt, sẽ được lưu trong Resgistry</param>
        /// <param name="description">Mô tả, sẽ được lưu trong Registry</param>
        public void CreateWLDSlot(int wldSlotID, string tokenLabel, string serial, string description)
        {
            string KeyPath = @"HKEY_LOCAL_MACHINE\SOFTWARE\SafeNet\PTKC\WLD";
            string valueName = "ET_PTKC_WLD_SLOT_" + wldSlotID.ToString();
            string value = tokenLabel + "," + serial + "," + description;

            Registry.SetValue(KeyPath, valueName, value, RegistryValueKind.String);
        }

        /// <summary>
        /// Xóa WLD Slot trong Registry
        /// </summary>
        /// <param name="wldSlotID">Chỉ số slot, được được cấu hình trong Registry dạng tên khóa ET_PKCS_WLD_SLOT_[wldSlotID]</param>
        public void DeleteWLDSlot(int wldSlotID)
        {
            string KeyPath_lc = @"SOFTWARE\SafeNet\PTKC\WLD";
            string valueName = "ET_PTKC_WLD_SLOT_" + wldSlotID.ToString();

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(KeyPath_lc, true))
            {
                if (key != null)
                {
                    key.DeleteValue(valueName);
                }
            }
        }
        #endregion

        #region Private Member
        private PKCS11.Slot[] GetAdminSlotList()
        {
            List<PKCS11.Slot> adminSlot = new List<PKCS11.Slot>();

            //Get the entire slot list.
            PKCS11.Slot[] slots = PKCS11.GetSlotList(true);

            //Loop until the correct admin slot is found.
            for (int i = 0; i < slots.Length; i++)
            {
                /* Get information on the current slot */
                PKCS11.SlotInfo slotInfo = slots[i].GetSlotInfo();
                /* The admin slot cannot be a removable device. */
                if ((slotInfo.flags & PKCS11.CKF_REMOVABLE_DEVICE) == 0)
                {
                    PKCS11.TokenInfo tokenInfo = slots[i].GetTokenInfo();
                    if ((tokenInfo.flags & PKCS11.CKF_ADMIN_TOKEN) != 0)
                        adminSlot.Add(slots[i]);
                }
            }

            return adminSlot.ToArray();
        }

        private PKCS11.Slot FindAdminSlot(int devideID)
        {
            PKCS11.Slot adminSlot = new PKCS11.Slot();
            int adminSlotCount = 0;
            bool hasSlot = false;

            //Get the entire slot list.
            PKCS11.Slot[] slots = PKCS11.GetSlotList(true);

            //Loop until the correct admin slot is found.
            for (int i = 0; i < slots.Length; i++)
            {
                /* Get information on the current slot */
                PKCS11.SlotInfo slotInfo = slots[i].GetSlotInfo();
                /* The admin slot cannot be a removable device. */
                if ((slotInfo.flags & PKCS11.CKF_REMOVABLE_DEVICE) == 0)
                {
                    PKCS11.TokenInfo tokenInfo = slots[i].GetTokenInfo();
                    if ((tokenInfo.flags & PKCS11.CKF_ADMIN_TOKEN) != 0)
                    {
                        ++adminSlotCount;

                        if ((adminSlotCount - 1) == devideID)
                        {
                            adminSlot = slots[i];
                            hasSlot = true;
                            break;
                        }
                    }
                }
            }

            if (!hasSlot)
                throw new Exception("HSM_AdminSlotNotFound");

            return adminSlot;
        }

        private PKCS11.Slot FindSlotByID(int id)
        {
            bool hasSlot = false;
            PKCS11.Slot[] slots = PKCS11.GetSlotList(false);
            PKCS11.Slot slot = new PKCS11.Slot();
            foreach (PKCS11.Slot sl in slots)
            {
                if (sl.id == id)
                {
                    slot = sl;
                    hasSlot = true;
                    break;
                }
            }

            if (!hasSlot)
                throw new Exception("HSM_SlotNotFound");

            return slot;
        }

        private PKCS11.Slot FindSlotBySerial(string serial)
        {
            bool hasSlot = false;
            PKCS11.Slot[] slots = PKCS11.GetSlotList(false);
            PKCS11.Slot slot = new PKCS11.Slot();
            foreach (PKCS11.Slot sl in slots)
            {
                //Get serial
                string sl_Serial = sl.GetTokenInfo().serialNumber;
                //Compare
                if (sl_Serial == serial)
                {
                    slot = sl;
                    hasSlot = true;
                    break;
                }
            }

            if (!hasSlot)
                throw new Exception("HSM_SlotNotFound");

            return slot;
        }

        private PKCS11.Slot FindSlotByTokenLabel(string label)
        {
            bool hasSlot = false;
            PKCS11.Slot[] slots = PKCS11.GetSlotList(false);
            PKCS11.Slot slot = new PKCS11.Slot();
            foreach (PKCS11.Slot sl in slots)
            {
                //Get token label
                string sl_label = sl.GetTokenInfo().label;

                //Compare
                if (sl_label == label)
                {
                    slot = sl;
                    hasSlot = true;
                    break;
                }
            }

            if (!hasSlot)
                throw new Exception("HSM_SlotNotFound");

            return slot;
        }

        private void CheckRV(string header, int rv)
        {
            try
            {
                PKCS11.Exception.check(rv);
            }
            catch (Exception ex)
            {
                throw new Exception(header + ex.Message);
            }
        }
        #endregion

        //private string GetShortSerial(string sn)
        //{
        //    //Get serial
        //    //xxxx:xxxx
        //    //Where the first field is the HSM serial number and the second field is a
        //    //randomly assigned token serial number or the smartcard serial number.
        //    //The serial number specified for the virtual WLD Slot in environment
        //    //variables ET_PTKC_WLD_SLOT_n.
        //    string[] arrSerial = sn.Split(new char[] { ':' });
        //    string sl_Serial = arrSerial.Count() > 1 ? arrSerial[1] : arrSerial[0];

        //    return sl_Serial;
        //}

        //private static byte[] SignData(byte[] dataToSign, bool tmp)
        //{
        //    PKCS11.Finalize();
        //    PKCS11.Initialize("cryptoki.dll");
        //    PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
        //    if (slots.Length > 0)
        //    {
        //        PKCS11.Slot slot = new PKCS11.Slot();
        //        foreach (PKCS11.Slot slot1 in slots)
        //        {
        //            if (slot1.id == 2)
        //            {
        //                slot = slot1;
        //                break;
        //            }
        //        }

        //        PKCS11.Session session = PKCS11.OpenSession(slot,
        //          PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);

        //        int rv = session.Login(PKCS11.CKU_USER, "123457");

        //        PKCS11.Object[] PIkeys = session.FindObjects(new PKCS11.Attribute[]  {
        //            new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PRIVATE_KEY),
        //            new PKCS11.Attribute(PKCS11.CKA_LABEL, "NinhtqPI")
        //          });

        //        if (PIkeys.Count() > 0)
        //        {
        //            int keytype = (int)PIkeys[0].Get(session, PKCS11.CKA_KEY_TYPE);

        //            PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_RSA_PKCS, null);
        //            byte[] SignedArray = session.Sign(signMech, PIkeys[0], dataToSign);

        //            PKCS11.Finalize();
        //            return SignedArray;
        //        }
        //        else
        //        {
        //            PKCS11.Finalize();
        //            throw new Exception("Can not find the key!");
        //        }
        //    }
        //    else
        //    {
        //        PKCS11.Finalize();
        //        throw new Exception("Can not find any slot!");
        //    }
        //}

        //private static int VerifyData(byte[] dataToSign, byte[] signature)
        //{
        //    PKCS11.Finalize();
        //    PKCS11.Initialize("cryptoki.dll");
        //    PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
        //    if (slots.Length > 0)
        //    {
        //        PKCS11.Slot slot = slots[2];
        //        PKCS11.Session session = PKCS11.OpenSession(slot,
        //          PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);

        //        session.Login(PKCS11.CKU_USER, "123456");

        //        PKCS11.Object[] PUkeys = session.FindObjects(new PKCS11.Attribute[]  {
        //            new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PUBLIC_KEY),
        //            new PKCS11.Attribute(PKCS11.CKA_LABEL, "NinhtqCert")
        //          });

        //        if (PUkeys.Count() > 0)
        //        {
        //            PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_SHA1_RSA_PKCS, null);
        //            int result = session.Verify(signMech, PUkeys[0], dataToSign, signature);

        //            PKCS11.Finalize();
        //            return result;
        //        }
        //        else
        //        {
        //            PKCS11.Finalize();
        //            throw new Exception("Can not find the key!");
        //        }
        //    }
        //    else
        //    {
        //        PKCS11.Finalize();
        //        throw new Exception("Can not find any slot!");
        //    }
        //}

        //private static byte[] SignHash(byte[] hashvalue, string oid, bool tmp)
        //{
        //    //Constant
        //    byte[] sha1AlgoID = PKCS11.DigestAlgoID[oid];

        //    //Tạo DigestInfo = HashAlgorithmID || HashVaue (see EMSA-PKCS-v1.5, PKCS #1 v2.2 RSA Cryptography Standart)
        //    byte[] digestInfo = new byte[sha1AlgoID.Length + hashvalue.Length];
        //    sha1AlgoID.CopyTo(digestInfo, 0);
        //    hashvalue.CopyTo(digestInfo, sha1AlgoID.Length);


        //    //Kí chuỗi DigestInfo bằng CKM_RSA_PKCS
        //    return HSMServiceProvider.SignData(digestInfo, true);
        //}


        //public byte[] GenerateKeyPair(HSMKeyPairType keyType, string subject, string labelPUB, string labelPRV)
        //{
        //    //Loại khóa
        //    PKCS11.Mechanism keyMech = new PKCS11.Mechanism((int)keyType, null);

        //    //Thuộc tính Public Key và Private Key
        //    PKCS11.Attribute[] publicKeyAtt = new PKCS11.Attribute[] {
        //        new PKCS11.Attribute(PKCS11.CKA_SUBJECT, subject), 
        //        new PKCS11.Attribute(PKCS11.CKA_LABEL, labelPUB),
        //        new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
        //        new PKCS11.Attribute(PKCS11.CKA_PRIME_BITS, 1024),
        //        new PKCS11.Attribute(PKCS11.CKA_PRIVATE, false),
        //        new PKCS11.Attribute(PKCS11.CKA_MODIFIABLE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_ENCRYPT, true),
        //        new PKCS11.Attribute(PKCS11.CKA_VERIFY, true),
        //        new PKCS11.Attribute(PKCS11.CKA_DERIVE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_WRAP, true)
        //    };

        //    PKCS11.Attribute[] privateKeyAtt = new PKCS11.Attribute[] {
        //        new PKCS11.Attribute(PKCS11.CKA_SUBJECT, subject), 
        //        new PKCS11.Attribute(PKCS11.CKA_LABEL, labelPRV),
        //        new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
        //        new PKCS11.Attribute(PKCS11.CKA_PRIVATE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_MODIFIABLE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_SENSITIVE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_EXTRACTABLE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_EXPORTABLE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_DECRYPT, true),
        //        new PKCS11.Attribute(PKCS11.CKA_SIGN, true),
        //        new PKCS11.Attribute(PKCS11.CKA_DERIVE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_UNWRAP, true),
        //        new PKCS11.Attribute(PKCS11.CKA_IMPORT, false),
        //        new PKCS11.Attribute(PKCS11.CKA_SIGN_LOCAL_CERT, true)
        //    };

        //    //Tạo cặp key
        //    PKCS11.Object pubKey;
        //    PKCS11.Object prvKey;
        //    PKCS11.GenerateKeyPair(_session, keyMech, publicKeyAtt, out pubKey, privateKeyAtt, out prvKey);

        //    //Trả về ID của cặp key
        //    return (byte[])pubKey.Get(_session, PKCS11.CKA_ID);
        //}

        //public string GenerateCertificateRequest(byte[] keyPairID, string labelREQ)
        //{
        //    //Tìm Key Object
        //    PKCS11.Object[] PIkeys = _session.FindObjects(new PKCS11.Attribute[]  {
        //            new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PRIVATE_KEY),
        //            new PKCS11.Attribute(PKCS11.CKA_ID, keyPairID)
        //          });
        //    PKCS11.Object[] PUkeys = _session.FindObjects(new PKCS11.Attribute[]  {
        //            new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PUBLIC_KEY),
        //            new PKCS11.Attribute(PKCS11.CKA_ID, keyPairID)
        //          });

        //    //Kiểm tra
        //    if (PIkeys.Length < 1 || PUkeys.Length < 1)
        //        throw new CryptographicException("HSM_KeyNotFound");
        //    if (PIkeys.Length > 1 || PUkeys.Length > 1)
        //        throw new CryptographicException("HSM_DuplicateKey");

        //    //Tạo certification request
        //    PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_SHA1_RSA_PKCS, null);
        //    _session.SignInit(signMech, PIkeys[0]);

        //    PKCS11.Mechanism reqMech = new PKCS11.Mechanism(PKCS11.CKM_ENCODE_PKCS_10, null);
        //    PKCS11.Attribute[] requestAtt = new PKCS11.Attribute[] {
        //        new PKCS11.Attribute(PKCS11.CKA_LABEL, labelREQ),
        //        new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
        //        new PKCS11.Attribute(PKCS11.CKA_PRIVATE, false),
        //        new PKCS11.Attribute(PKCS11.CKA_MODIFIABLE, true),
        //        new PKCS11.Attribute(PKCS11.CKA_DERIVE, true)
        //    };
        //    PUkeys[0].DeriveKey(_session, reqMech, requestAtt);

        //    //Trả về chuỗi pem
        //    return ExportCertificateRequestToPEM(keyPairID);
        //}
    }
}
