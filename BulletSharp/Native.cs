using System;
using System.IO;
using System.Runtime.InteropServices;

namespace BulletSharp
{
    public static class Native
    {
        public static class AndroidTest
        {
            static bool? _isAndroid;
            /// <summary>
            /// Run Chech
            /// </summary>
            /// <returns>If it is anroid</returns>
            public static bool Check()
            {
                if (_isAndroid != null)
                {
                    return (bool)_isAndroid;
                }
				using var process = new System.Diagnostics.Process();
				process.StartInfo.FileName = "getprop";
				process.StartInfo.Arguments = "ro.build.user";
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				try {
					process.Start();
					var output = process.StandardOutput.ReadToEnd();
					_isAndroid = string.IsNullOrEmpty(output) ? (bool?)false : (bool?)true;
				}
				catch {
					_isAndroid = false;
				}
				return (bool)_isAndroid;
			}
        }



        static bool _loaded = false;
        public static bool Load()
        {
            if (_loaded)
            {
                return true;
            }

            // Android uses a different strategy for linking the DLL
            if (AndroidTest.Check())
            {
                return true;
            }

            var arch = RuntimeInformation.OSArchitecture == Architecture.Arm64
                ? "arm64"
                : "x64";
            _loaded = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? LoadWindows(arch)
                : LoadUnix(arch);
            return _loaded;
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern IntPtr LoadLibraryW(string fileName);
        static bool LoadWindows(string arch)
        {
            return LoadLibraryW("libbulletc") != IntPtr.Zero || LoadLibraryW($"runtimes/win-{arch}/native/libbulletc.dll") != IntPtr.Zero;
        }


        [DllImport("libdl", CharSet = CharSet.Ansi)]
        static extern IntPtr dlopen(string fileName, int flags);

		static bool Dlcopy(string fileName) {
			if (File.Exists(fileName)) {
				try {
					File.Copy(fileName, $"{ AppDomain.CurrentDomain.BaseDirectory}/libbulletc.so");
				}
				catch {
				}
				return true;
			}
			else {
				return false;
			}
		}

		static bool FinalTryUnix(string fileName) {
			var dirs = Directory.GetDirectories("./../");
			foreach (var item in dirs) {
				if(item.Contains("runtimes")) {
					return dlopen(item + "/" + fileName, 2) != IntPtr.Zero
						|| Dlcopy(item + "/" + fileName);
				}
				var dirs2 = Directory.GetDirectories(item);
				foreach (var item2 in dirs) {
					if (item2.Contains("runtimes")) {
						return dlopen(item + "/" + fileName, 2) != IntPtr.Zero
							|| Dlcopy(item + "/" + fileName);
					}
					var dirs3 = Directory.GetDirectories(item2);
					foreach (var item3 in dirs) {
						if (item3.Contains("runtimes")) {
							return dlopen(item + "/" + fileName, 2) != IntPtr.Zero
								|| Dlcopy(item + "/" + fileName);
						}
					}
				}
			}
			return false;
		}

		static bool LoadUnix(string arch)
        {
            const int RTLD_NOW = 2;
			try {
				return dlopen("libbulletc.so", RTLD_NOW) != IntPtr.Zero
				|| dlopen($"./runtimes/linux-{arch}/native/libbulletc.so", RTLD_NOW) != IntPtr.Zero
				|| dlopen($"./../runtimes/linux-{arch}/native/libbulletc.so", RTLD_NOW) != IntPtr.Zero
				|| dlopen($"./../../runtimes/linux-{arch}/native/libbulletc.so", RTLD_NOW) != IntPtr.Zero
				|| dlopen($"{AppDomain.CurrentDomain.BaseDirectory}/runtimes/linux-{arch}/native/libbulletc.so", RTLD_NOW) != IntPtr.Zero
				|| Dlcopy("libbulletc.so")
				|| Dlcopy($"./runtimes/linux-{arch}/native/libbulletc.so")
				|| Dlcopy($"./../runtimes/linux-{arch}/native/libbulletc.so")
				|| Dlcopy($"./../../runtimes/linux-{arch}/native/libbulletc.so")
				|| Dlcopy($"{AppDomain.CurrentDomain.BaseDirectory}/runtimes/linux-{arch}/native/libbulletc.so")
				|| FinalTryUnix($"linux-{arch}/native/libbulletc.so");
			}
			catch {
				//This is if libdl is not working for some reson
				return Dlcopy("libbulletc.so")
				|| Dlcopy($"./runtimes/linux-{arch}/native/libbulletc.so")
				|| Dlcopy($"./../runtimes/linux-{arch}/native/libbulletc.so")
				|| Dlcopy($"./../../runtimes/linux-{arch}/native/libbulletc.so")
				|| Dlcopy($"{AppDomain.CurrentDomain.BaseDirectory}/runtimes/linux-{arch}/native/libbulletc.so");
			}
        }

        public const string DLL = "libbulletc";
        public const CallingConvention CONV = CallingConvention.Cdecl;
    }
}
