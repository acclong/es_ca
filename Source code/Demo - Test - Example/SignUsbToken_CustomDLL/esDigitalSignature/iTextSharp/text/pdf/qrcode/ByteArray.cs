using System;
/*
 * Copyright 2008 ZXing authors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace esDigitalSignature.iTextSharp.text.pdf.qrcode {

    /**
     * This class implements an array of unsigned bytes.
     *
     * @author dswitkin@google.com (Daniel Switkin)
     */
    public sealed class ByteArray {

        private const int INITIAL_SIZE = 32;

        private byte[] bytes;
        private int size;

        public ByteArray() {
            bytes = null;
            size = 0;
        }

        public ByteArray(int size) {
            bytes = new byte[size];
            this.size = size;
        }

        public ByteArray(byte[] byteArray) {
            bytes = byteArray;
            size = bytes.Length;
        }

        /**
         * Access an unsigned byte at location index.
         * @param index The index in the array to access.
         * @return The unsigned value of the byte as an int.
         */
        public int At(int index) {
            return bytes[index] & 0xff;
        }

        public void Set(int index, int value) {
            bytes[index] = (byte)value;
        }

        public int Size() {
            return size;
        }

        public bool IsEmpty() {
            return size == 0;
        }

        public void AppendByte(int value) {
            if (size == 0 || size >= bytes.Length) {
                int newSize = Math.Max(INITIAL_SIZE, size << 1);
                Reserve(newSize);
            }
            bytes[size] = (byte)value;
            size++;
        }

        public void Reserve(int capacity) {
            if (bytes == null || bytes.Length < capacity) {
                byte[] newArray = new byte[capacity];
                if (bytes != null) {
                    System.Array.Copy(bytes, 0, newArray, 0, bytes.Length);
                }
                bytes = newArray;
            }
        }

        // Copy count bytes from array source starting at offset.
        public void Set(byte[] source, int offset, int count) {
            bytes = new byte[count];
            size = count;
            for (int x = 0; x < count; x++) {
                bytes[x] = source[offset + x];
            }
        }

    }
}
