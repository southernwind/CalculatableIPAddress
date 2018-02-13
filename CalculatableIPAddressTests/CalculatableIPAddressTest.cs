using NUnit.Framework;

namespace SandBeige {

	[TestFixture]
	public class CalculatableIPAddressTest {

		/// <summary>
		/// + Test
		/// </summary>
		/// <param name="sourceArg"></param>
		/// <param name="right"></param>
		/// <param name="resultArg"></param>
		[TestCase(new byte[] { 10, 0, 0, 0 }, 1, new byte[] { 10, 0, 0, 1 })]
		[TestCase(new byte[] { 10, 0, 0, 0 }, 2, new byte[] { 10, 0, 0, 2 })]
		[TestCase(new byte[] { 10, 0, 0, 0 }, 3, new byte[] { 10, 0, 0, 3 })]
		[TestCase(new byte[] { 10, 0, 0, 0 }, 4, new byte[] { 10, 0, 0, 4 })]
		[TestCase(new byte[] { 10, 0, 0, 255 }, 1, new byte[] { 10, 0, 1, 0 })]
		[TestCase(new byte[] { 10, 0, 255, 255 }, 1, new byte[] { 10, 1, 0, 0 })]
		[TestCase(new byte[] { 10, 255, 255, 255 }, 1, new byte[] { 11, 0, 0, 0 })]
		[TestCase(new byte[] { 255, 255, 255, 255 }, 1, new byte[] { 0, 0, 0, 0 })]
		public void Plus(byte[] sourceArg, int right, byte[] resultArg) {
			var cip = new CalculatableIPAddress(sourceArg);
			var resultIp = new CalculatableIPAddress(resultArg);
			Assert.AreEqual(cip + right, resultIp);
			if (right == 1) {
				Assert.AreEqual(resultIp, ++cip);
			}
		}

		/// <summary>
		/// - Test
		/// </summary>
		/// <param name="sourceArg"></param>
		/// <param name="right"></param>
		/// <param name="resultArg"></param>
		[TestCase(new byte[] { 10, 0, 0, 1 }, 1, new byte[] { 10, 0, 0, 0 })]
		[TestCase(new byte[] { 10, 0, 0, 2 }, 2, new byte[] { 10, 0, 0, 0 })]
		[TestCase(new byte[] { 10, 0, 0, 3 }, 3, new byte[] { 10, 0, 0, 0 })]
		[TestCase(new byte[] { 10, 0, 0, 4 }, 4, new byte[] { 10, 0, 0, 0 })]
		[TestCase(new byte[] { 10, 0, 1, 0 }, 1, new byte[] { 10, 0, 0, 255 })]
		[TestCase(new byte[] { 10, 1, 0, 0 }, 1, new byte[] { 10, 0, 255, 255 })]
		[TestCase(new byte[] { 11, 0, 0, 0 }, 1, new byte[] { 10, 255, 255, 255 })]
		[TestCase(new byte[] { 0, 0, 0, 0 }, 1, new byte[] { 255, 255, 255, 255 })]
		public void Minus(byte[] sourceArg, int right, byte[] resultArg) {
			var cip = new CalculatableIPAddress(sourceArg);
			var resultIp = new CalculatableIPAddress(resultArg);
			Assert.AreEqual(cip - right, resultIp);
			if (right == 1) {
				Assert.AreEqual(resultIp, --cip);
			}
		}

		/// <summary>
		/// & Test
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="result"></param>
		[TestCase(new byte[] { 10, 0, 0, 1 }, new byte[] { 255, 0, 0, 0 }, new byte[] { 10, 0, 0, 0 })]
		[TestCase(new byte[] { 10, 10, 166, 1 }, new byte[] { 255, 255, 0, 0 }, new byte[] { 10, 10, 0, 0 })]
		[TestCase(new byte[] { 10, 3, 93, 3 }, new byte[] { 255, 255, 248, 0 }, new byte[] { 10, 3, 88, 0 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 255, 255, 255, 255 }, new byte[] { 10, 9, 111, 163 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 255, 255, 255, 248 }, new byte[] { 10, 9, 111, 160 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 255, 255, 192, 0 }, new byte[] { 10, 9, 64, 0 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 0, 0, 0, 0 }, new byte[] { 0, 0, 0, 0 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 128, 0, 0, 0 }, new byte[] { 0, 0, 0, 0 })]
		[TestCase(new byte[] { 192, 9, 111, 163 }, new byte[] { 128, 0, 0, 0 }, new byte[] { 128, 0, 0, 0 })]
		public void And(byte[] left, byte[] right, byte[] result) {
			var leftIp = new CalculatableIPAddress(left);
			var rightIp = new CalculatableIPAddress(right);
			var resultIp = new CalculatableIPAddress(result);
			Assert.AreEqual(resultIp, leftIp & rightIp);
		}

		/// <summary>
		/// | Test
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="result"></param>
		[TestCase(new byte[] { 10, 0, 0, 1 }, new byte[] { 0, 255, 255, 255 }, new byte[] { 10, 255, 255, 255 })]
		[TestCase(new byte[] { 10, 10, 166, 1 }, new byte[] { 0, 0, 255, 255 }, new byte[] { 10, 10, 255, 255 })]
		[TestCase(new byte[] { 10, 3, 93, 3 }, new byte[] { 0, 0, 7, 255 }, new byte[] { 10, 3, 95, 255 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 0, 0, 0, 7 }, new byte[] { 10, 9, 111, 167 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 0, 0, 63, 255 }, new byte[] { 10, 9, 127, 255 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 255, 255, 255, 255 }, new byte[] { 255, 255, 255, 255 })]
		[TestCase(new byte[] { 10, 9, 111, 163 }, new byte[] { 127, 255, 255, 255 }, new byte[] { 127, 255, 255, 255 })]
		[TestCase(new byte[] { 192, 9, 111, 163 }, new byte[] { 127, 255, 255, 255 }, new byte[] { 255, 255, 255, 255 })]
		public void Or(byte[] left, byte[] right, byte[] result) {
			var leftIp = new CalculatableIPAddress(left);
			var rightIp = new CalculatableIPAddress(right);
			var resultIp = new CalculatableIPAddress(result);
			Assert.AreEqual(resultIp, leftIp | rightIp);
		}

		/// <summary>
		/// ^ Test
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="result"></param>
		[TestCase(new byte[] { 255, 255, 255, 255 }, new byte[] { 0, 0, 0, 0 }, new byte[] { 255, 255, 255, 255 })]
		[TestCase(new byte[] { 255, 255, 255, 248 }, new byte[] { 255, 255, 255, 255 }, new byte[] { 0, 0, 0, 7 })]
		[TestCase(new byte[] { 255, 255, 248, 0 }, new byte[] { 255, 255, 255, 255 }, new byte[] { 0, 0, 7, 255 })]
		[TestCase(new byte[] { 128, 0, 0, 0 }, new byte[] { 255, 255, 255, 255 }, new byte[] { 127, 255, 255, 255 })]
		public void Exclusive(byte[] left, byte[] right, byte[] result) {
			var leftIp = new CalculatableIPAddress(left);
			var rightIp = new CalculatableIPAddress(right);
			var resultIp = new CalculatableIPAddress(result);
			Assert.AreEqual(resultIp, leftIp ^ rightIp);
		}

		/// <summary>
		/// == Test
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="result"></param>
		[TestCase(new byte[] { 128, 0, 0, 0 }, new byte[] { 128, 0, 0, 0 }, true)]
		[TestCase(new byte[] { 255, 16, 39, 211 }, new byte[] { 255, 16, 39, 211 }, true)]
		[TestCase(new byte[] { 128, 0, 0, 1 }, new byte[] { 128, 0, 0, 0 }, false)]
		[TestCase(new byte[] { 255, 16, 39, 212 }, new byte[] { 255, 16, 39, 211 }, false)]
		public void Equal(byte[] left, byte[] right, bool result) {
			var leftIp = new CalculatableIPAddress(left);
			var rightIp = new CalculatableIPAddress(right);
			Assert.AreEqual(result, leftIp == rightIp);
			Assert.AreEqual(!result, leftIp != rightIp);
		}

		/// <summary>
		/// > Test
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="result"></param>
		[TestCase(new byte[] { 128, 0, 0, 0 }, new byte[] { 128, 0, 0, 0 }, false)]
		[TestCase(new byte[] { 128, 0, 0, 0 }, new byte[] { 128, 0, 0, 1 }, false)]
		[TestCase(new byte[] { 128, 0, 0, 1 }, new byte[] { 128, 0, 0, 0 }, true)]
		[TestCase(new byte[] { 128, 0, 1, 0 }, new byte[] { 128, 0, 0, 255 }, true)]
		[TestCase(new byte[] { 128, 1, 0, 0 }, new byte[] { 128, 0, 255, 0 }, true)]
		[TestCase(new byte[] { 129, 0, 0, 0 }, new byte[] { 128, 255, 0, 0 }, true)]
		[TestCase(new byte[] { 0, 0, 0, 0 }, new byte[] { 255, 0, 0, 0 }, false)]
		public void Than(byte[] left, byte[] right, bool result) {
			var leftIp = new CalculatableIPAddress(left);
			var rightIp = new CalculatableIPAddress(right);
			Assert.AreEqual(result, leftIp > rightIp);
			Assert.AreEqual(result, rightIp < leftIp);
		}
	}
}
