using System;
using System.Linq;
using System.Net;

namespace SandBeige {
	public class CalculatableIPAddress : IPAddress, IComparable {

		public CalculatableIPAddress(IPAddress address) : base(address.GetAddressBytes()) {

		}

		public CalculatableIPAddress(long address) : base(address) {

		}
		public CalculatableIPAddress(byte[] address) : base(address) {

		}
		public CalculatableIPAddress(byte[] address, long scopeid) : base(address, scopeid) {

		}

		private CalculatableIPAddress(uint address) : base(BitConverter.GetBytes(address)) {

		}

		public static bool operator >(CalculatableIPAddress left, CalculatableIPAddress right) {
			if (left == null || right == null) {
				return false;
			}
			var leftReverseUintIp = BitConverter.ToUInt32(left.GetAddressBytes().Reverse().ToArray(), 0);
			var rightReverseUintIp = BitConverter.ToUInt32(right.GetAddressBytes().Reverse().ToArray(), 0);
			return leftReverseUintIp > rightReverseUintIp;
		}
		public static bool operator <(CalculatableIPAddress left, CalculatableIPAddress right) {
			if (left == null || right == null) {
				return false;
			}
			var leftReverseUintIp = BitConverter.ToUInt32(left.GetAddressBytes().Reverse().ToArray(), 0);
			var rightReverseUintIp = BitConverter.ToUInt32(right.GetAddressBytes().Reverse().ToArray(), 0);
			return leftReverseUintIp < rightReverseUintIp;
		}
		public static bool operator ==(CalculatableIPAddress left, CalculatableIPAddress right) {
			if(ReferenceEquals(left,right)) {
				return true;
			}
			if (ReferenceEquals(left, null) || ReferenceEquals(right, null)) {
				return false;
			}

			var leftReverseUintIp = BitConverter.ToUInt32(left.GetAddressBytes().Reverse().ToArray(), 0);
			var rightReverseUintIp = BitConverter.ToUInt32(right.GetAddressBytes().Reverse().ToArray(), 0);
			return leftReverseUintIp == rightReverseUintIp;
		}
		public static bool operator !=(CalculatableIPAddress left, CalculatableIPAddress right) {
			if (left == null || right == null) {
				return false;
			}
			var leftReverseUintIp = BitConverter.ToUInt32(left.GetAddressBytes().Reverse().ToArray(), 0);
			var rightReverseUintIp = BitConverter.ToUInt32(right.GetAddressBytes().Reverse().ToArray(), 0);
			return leftReverseUintIp != rightReverseUintIp;
		}

		public static bool operator >=(CalculatableIPAddress left, CalculatableIPAddress right) {
			if (left == null || right == null) {
				return false;
			}
			var leftReverseUintIp = BitConverter.ToUInt32(left.GetAddressBytes().Reverse().ToArray(), 0);
			var rightReverseUintIp = BitConverter.ToUInt32(right.GetAddressBytes().Reverse().ToArray(), 0);
			return leftReverseUintIp >= rightReverseUintIp;
		}
		public static bool operator <=(CalculatableIPAddress left, CalculatableIPAddress right) {
			if (left == null || right == null) {
				return false;
			}
			var leftReverseUintIp = BitConverter.ToUInt32(left.GetAddressBytes().Reverse().ToArray(), 0);
			var rightReverseUintIp = BitConverter.ToUInt32(right.GetAddressBytes().Reverse().ToArray(), 0);
			return leftReverseUintIp <= rightReverseUintIp;
		}

		public static CalculatableIPAddress operator &(CalculatableIPAddress address, CalculatableIPAddress mask) {
			if (address == null || mask == null) {
				return null;
			}
			var byteArrayIp = address.GetAddressBytes();
			var byteArrayMask = mask.GetAddressBytes();
			var resultBit = byteArrayIp.Select((x, i) =>
			(byte)(x & byteArrayMask[i])
			);
			return new CalculatableIPAddress(resultBit.ToArray());
		}

		public static CalculatableIPAddress operator |(CalculatableIPAddress address, CalculatableIPAddress mask) {
			if (address == null || mask == null) {
				return null;
			}
			var byteArrayIp = address.GetAddressBytes();
			var byteArrayMask = mask.GetAddressBytes();
			var resultBit = byteArrayIp.Select((x, i) => (byte)(x | byteArrayMask[i]));
			return new CalculatableIPAddress(resultBit.ToArray());
		}

		public static CalculatableIPAddress operator ^(CalculatableIPAddress address, CalculatableIPAddress mask) {
			if (address == null || mask == null) {
				return null;
			}
			var byteArrayIp = address.GetAddressBytes();
			var byteArrayMask = mask.GetAddressBytes();
			var resultBit = byteArrayIp.Select((x, i) => (byte)(x ^ byteArrayMask[i]));
			return new CalculatableIPAddress(resultBit.ToArray());
		}

		public static CalculatableIPAddress operator +(CalculatableIPAddress address, int right) {
			if (address == null) {
				return null;
			}
			var reverseUintIp = BitConverter.ToUInt32(address.GetAddressBytes().Reverse().ToArray(), 0);
			var calculatedReverseByteArrayIp = BitConverter.GetBytes((uint)(reverseUintIp + right));
			var calculatedByteArrayIp = calculatedReverseByteArrayIp.Reverse().ToArray();
			return new CalculatableIPAddress(calculatedByteArrayIp);
		}

		public static CalculatableIPAddress operator ++(CalculatableIPAddress address) {
			return address + 1;
		}

		public static CalculatableIPAddress operator -(CalculatableIPAddress address, int right) {
			if (address == null) {
				return null;
			}
			var reverseUintIp = BitConverter.ToUInt32(address.GetAddressBytes().Reverse().ToArray(), 0);
			var calculatedReverseByteArrayIp = BitConverter.GetBytes((uint)(reverseUintIp - right));
			var calculatedByteArrayIp = calculatedReverseByteArrayIp.Reverse().ToArray();
			return new CalculatableIPAddress(calculatedByteArrayIp);
		}
		public static CalculatableIPAddress operator --(CalculatableIPAddress address) {
			return address - 1;
		}

		public static long? operator -(CalculatableIPAddress left, CalculatableIPAddress right) {
			if (left == null || right == null) {
				return null;
			}
			var leftReverseUintIp = BitConverter.ToUInt32(left.GetAddressBytes().Reverse().ToArray(), 0);
			var rightReverseUintIp = BitConverter.ToUInt32(right.GetAddressBytes().Reverse().ToArray(), 0);

			return (long)leftReverseUintIp - rightReverseUintIp;
		}


		public override string ToString() {
			return base.ToString();
		}

		public override bool Equals(object comparand) {
			return base.Equals(comparand);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object obj) {
			if (obj is CalculatableIPAddress ip) {
				if (ip < this) {
					return -1;
				} else if (ip == this) {
					return 0;
				} else {
					return 1;
				}
			} else {
				throw new ArgumentException(" ");
			}
		}
	}

	public static class IPAddressExtensions {
		public static CalculatableIPAddress ToCalculatableIPAddress(this IPAddress address) {
			return new CalculatableIPAddress(address);
		}
	}
}
