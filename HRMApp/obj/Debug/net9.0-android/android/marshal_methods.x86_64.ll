; ModuleID = 'marshal_methods.x86_64.ll'
source_filename = "marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [420 x ptr] zeroinitializer, align 16

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [1260 x i64] [
	i64 u0x001e58127c546039, ; 0: lib_System.Globalization.dll.so => 42
	i64 u0x0024d0f62dee05bd, ; 1: Xamarin.KotlinX.Coroutines.Core.dll => 375
	i64 u0x0071cf2d27b7d61e, ; 2: lib_Xamarin.AndroidX.SwipeRefreshLayout.dll.so => 292
	i64 u0x00b3aadb3a4c4038, ; 3: lib_Refit.dll.so => 217
	i64 u0x01109b0e4d99e61f, ; 4: System.ComponentModel.Annotations.dll => 13
	i64 u0x018d2cc5e2de2b95, ; 5: lib_Microsoft.AspNetCore.SignalR.Common.dll.so => 180
	i64 u0x02123411c4e01926, ; 6: lib_Xamarin.AndroidX.Navigation.Runtime.dll.so => 281
	i64 u0x022f31be406de945, ; 7: Microsoft.Extensions.Options.ConfigurationExtensions => 195
	i64 u0x0284512fad379f7e, ; 8: System.Runtime.Handles => 105
	i64 u0x02abedc11addc1ed, ; 9: lib_Mono.Android.Runtime.dll.so => 170
	i64 u0x02f55bf70672f5c8, ; 10: lib_System.IO.FileSystem.DriveInfo.dll.so => 48
	i64 u0x032267b2a94db371, ; 11: lib_Xamarin.AndroidX.AppCompat.dll.so => 235
	i64 u0x03621c804933a890, ; 12: System.Buffers => 7
	i64 u0x0399610510a38a38, ; 13: lib_System.Private.DataContractSerialization.dll.so => 86
	i64 u0x043032f1d071fae0, ; 14: ru/Microsoft.Maui.Controls.resources => 405
	i64 u0x044440a55165631e, ; 15: lib-cs-Microsoft.Maui.Controls.resources.dll.so => 383
	i64 u0x046eb1581a80c6b0, ; 16: vi/Microsoft.Maui.Controls.resources => 411
	i64 u0x0470607fd33c32db, ; 17: Microsoft.IdentityModel.Abstractions.dll => 197
	i64 u0x047408741db2431a, ; 18: Xamarin.AndroidX.DynamicAnimation => 255
	i64 u0x04acae429ea0efac, ; 19: Xamarin.Grpc.Context => 360
	i64 u0x0514f1a3ae77a228, ; 20: lib_Xamarin.Kotlin.StdLib.Common.dll.so => 369
	i64 u0x0517ef04e06e9f76, ; 21: System.Net.Primitives => 71
	i64 u0x051a3be159e4ef99, ; 22: Xamarin.GooglePlayServices.Tasks => 357
	i64 u0x0565d18c6da3de38, ; 23: Xamarin.AndroidX.RecyclerView => 285
	i64 u0x0580fcad5f8dc5ac, ; 24: lib_Xamarin.Google.Dagger.dll.so => 339
	i64 u0x0581db89237110e9, ; 25: lib_System.Collections.dll.so => 12
	i64 u0x05989cb940b225a9, ; 26: Microsoft.Maui.dll => 203
	i64 u0x05a1c25e78e22d87, ; 27: lib_System.Runtime.CompilerServices.Unsafe.dll.so => 102
	i64 u0x06076b5d2b581f08, ; 28: zh-HK/Microsoft.Maui.Controls.resources => 412
	i64 u0x06388ffe9f6c161a, ; 29: System.Xml.Linq.dll => 155
	i64 u0x06600c4c124cb358, ; 30: System.Configuration.dll => 19
	i64 u0x067f95c5ddab55b3, ; 31: lib_Xamarin.AndroidX.Fragment.Ktx.dll.so => 260
	i64 u0x0680a433c781bb3d, ; 32: Xamarin.AndroidX.Collection.Jvm => 242
	i64 u0x069fff96ec92a91d, ; 33: System.Xml.XPath.dll => 160
	i64 u0x070b0847e18dab68, ; 34: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 257
	i64 u0x071cdb5520b57ab2, ; 35: lib_Plugin.Firebase.RemoteConfig.dll.so => 215
	i64 u0x0739448d84d3b016, ; 36: lib_Xamarin.AndroidX.VectorDrawable.dll.so => 295
	i64 u0x07469f2eecce9e85, ; 37: mscorlib.dll => 166
	i64 u0x07555d967851701d, ; 38: Xamarin.Firebase.Storage.Common.dll => 330
	i64 u0x07c57877c7ba78ad, ; 39: ru/Microsoft.Maui.Controls.resources.dll => 405
	i64 u0x07dcdc7460a0c5e4, ; 40: System.Collections.NonGeneric => 10
	i64 u0x08122e52765333c8, ; 41: lib_Microsoft.Extensions.Logging.Debug.dll.so => 193
	i64 u0x088610fc2509f69e, ; 42: lib_Xamarin.AndroidX.VectorDrawable.Animated.dll.so => 296
	i64 u0x08a7c865576bbde7, ; 43: System.Reflection.Primitives => 96
	i64 u0x08c9d051a4a817e5, ; 44: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 252
	i64 u0x08f3c9788ee2153c, ; 45: Xamarin.AndroidX.DrawerLayout => 254
	i64 u0x09076b821f278db5, ; 46: Xamarin.Google.Dagger => 339
	i64 u0x09138715c92dba90, ; 47: lib_System.ComponentModel.Annotations.dll.so => 13
	i64 u0x0919c28b89381a0b, ; 48: lib_Microsoft.Extensions.Options.dll.so => 194
	i64 u0x092266563089ae3e, ; 49: lib_System.Collections.NonGeneric.dll.so => 10
	i64 u0x098b50f911ccea8d, ; 50: lib_Xamarin.GooglePlayServices.Basement.dll.so => 348
	i64 u0x09d144a7e214d457, ; 51: System.Security.Cryptography => 127
	i64 u0x09da6dfc3439e851, ; 52: lib_Xamarin.Firebase.Components.dll.so => 311
	i64 u0x09e2b9f743db21a8, ; 53: lib_System.Reflection.Metadata.dll.so => 95
	i64 u0x0a76609879dc4df2, ; 54: Plugin.Firebase.Storage.dll => 216
	i64 u0x0abb3e2b271edc45, ; 55: System.Threading.Channels.dll => 225
	i64 u0x0b06b1feab070143, ; 56: System.Formats.Tar => 39
	i64 u0x0b3b632c3bbee20c, ; 57: sk/Microsoft.Maui.Controls.resources => 406
	i64 u0x0b521223ae889727, ; 58: Xamarin.Firebase.Messaging => 327
	i64 u0x0b6aff547b84fbe9, ; 59: Xamarin.KotlinX.Serialization.Core.Jvm => 379
	i64 u0x0b74b547d9e0e85d, ; 60: Microsoft.AspNetCore.SignalR.Protocols.Json.dll => 181
	i64 u0x0be2e1f8ce4064ed, ; 61: Xamarin.AndroidX.ViewPager => 298
	i64 u0x0c3ca6cc978e2aae, ; 62: pt-BR/Microsoft.Maui.Controls.resources => 402
	i64 u0x0c59ad9fbbd43abe, ; 63: Mono.Android => 171
	i64 u0x0c65741e86371ee3, ; 64: lib_Xamarin.Android.Glide.GifDecoder.dll.so => 229
	i64 u0x0c74af560004e816, ; 65: Microsoft.Win32.Registry.dll => 5
	i64 u0x0c7790f60165fc06, ; 66: lib_Microsoft.Maui.Essentials.dll.so => 204
	i64 u0x0c83c82812e96127, ; 67: lib_System.Net.Mail.dll.so => 67
	i64 u0x0cce4bce83380b7f, ; 68: Xamarin.AndroidX.Security.SecurityCrypto => 289
	i64 u0x0d13cd7cce4284e4, ; 69: System.Security.SecureString => 130
	i64 u0x0d63f4f73521c24f, ; 70: lib_Xamarin.AndroidX.SavedState.SavedState.Ktx.dll.so => 288
	i64 u0x0df310f37bf4881f, ; 71: lib_Xamarin.Firebase.Storage.Common.dll.so => 330
	i64 u0x0e04e702012f8463, ; 72: Xamarin.AndroidX.Emoji2 => 256
	i64 u0x0e14e73a54dda68e, ; 73: lib_System.Net.NameResolution.dll.so => 68
	i64 u0x0e5f1ca7132adebb, ; 74: Xamarin.Firebase.Installations.dll => 324
	i64 u0x0f37dd7a62ae99af, ; 75: lib_Xamarin.AndroidX.Collection.Ktx.dll.so => 243
	i64 u0x0f5e7abaa7cf470a, ; 76: System.Net.HttpListener => 66
	i64 u0x0fcf2ab35058c989, ; 77: Plugin.Firebase.DynamicLinks.dll => 212
	i64 u0x1001f97bbe242e64, ; 78: System.IO.UnmanagedMemoryStream => 57
	i64 u0x102a31b45304b1da, ; 79: Xamarin.AndroidX.CustomView => 251
	i64 u0x1065c4cb554c3d75, ; 80: System.IO.IsolatedStorage.dll => 52
	i64 u0x10f6cfcbcf801616, ; 81: System.IO.Compression.Brotli => 43
	i64 u0x114443cdcf2091f1, ; 82: System.Security.Cryptography.Primitives => 125
	i64 u0x11a603952763e1d4, ; 83: System.Net.Mail => 67
	i64 u0x11a70d0e1009fb11, ; 84: System.Net.WebSockets.dll => 81
	i64 u0x11f26371eee0d3c1, ; 85: lib_Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll.so => 271
	i64 u0x11fbe62d469cc1c8, ; 86: Microsoft.VisualStudio.DesignTools.TapContract.dll => 417
	i64 u0x12128b3f59302d47, ; 87: lib_System.Xml.Serialization.dll.so => 157
	i64 u0x123639456fb056da, ; 88: System.Reflection.Emit.Lightweight.dll => 92
	i64 u0x12521e9764603eaa, ; 89: lib_System.Resources.Reader.dll.so => 99
	i64 u0x125b7f94acb989db, ; 90: Xamarin.AndroidX.RecyclerView.dll => 285
	i64 u0x12d3b63863d4ab0b, ; 91: lib_System.Threading.Overlapped.dll.so => 140
	i64 u0x12db68ac7e4d099f, ; 92: lib_Xamarin.Firebase.Iid.dll.so => 322
	i64 u0x132a4db07c8f5c33, ; 93: HRMApp.dll => 0
	i64 u0x134eab1061c395ee, ; 94: System.Transactions => 150
	i64 u0x138567fa954faa55, ; 95: Xamarin.AndroidX.Browser => 239
	i64 u0x13a01de0cbc3f06c, ; 96: lib-fr-Microsoft.Maui.Controls.resources.dll.so => 389
	i64 u0x13beedefb0e28a45, ; 97: lib_System.Xml.XmlDocument.dll.so => 161
	i64 u0x13f1e5e209e91af4, ; 98: lib_Java.Interop.dll.so => 168
	i64 u0x13f1e880c25d96d1, ; 99: he/Microsoft.Maui.Controls.resources => 390
	i64 u0x1403071365bcd83a, ; 100: Xamarin.Firebase.Annotations => 305
	i64 u0x142d604b786f5eb8, ; 101: lib_Xamarin.CodeHaus.Mojo.AnimalSnifferAnnotations.dll.so => 302
	i64 u0x143d8ea60a6a4011, ; 102: Microsoft.Extensions.DependencyInjection.Abstractions => 186
	i64 u0x1497051b917530bd, ; 103: lib_System.Net.WebSockets.dll.so => 81
	i64 u0x14a644965fc9a91b, ; 104: Xamarin.Google.Android.Recaptcha.dll => 336
	i64 u0x14b78ce3adce0011, ; 105: Microsoft.VisualStudio.DesignTools.TapContract => 417
	i64 u0x14e68447938213b7, ; 106: Xamarin.AndroidX.Collection.Ktx.dll => 243
	i64 u0x15089560460fb845, ; 107: Microsoft.AspNetCore.SignalR.Client.Core => 179
	i64 u0x152a448bd1e745a7, ; 108: Microsoft.Win32.Primitives => 4
	i64 u0x1557de0138c445f4, ; 109: lib_Microsoft.Win32.Registry.dll.so => 5
	i64 u0x15bdc156ed462f2f, ; 110: lib_System.IO.FileSystem.dll.so => 51
	i64 u0x15e300c2c1668655, ; 111: System.Resources.Writer.dll => 101
	i64 u0x16726eac78495e6d, ; 112: Xamarin.Grpc.Stub => 364
	i64 u0x16b1b4f3fc11f978, ; 113: Xamarin.GooglePlayServices.Measurement => 350
	i64 u0x16bf2a22df043a09, ; 114: System.IO.Pipes.dll => 56
	i64 u0x16c1af7a51a0334a, ; 115: lib_Xamarin.GooglePlayServices.Measurement.Base.dll.so => 352
	i64 u0x16ea2b318ad2d830, ; 116: System.Security.Cryptography.Algorithms => 120
	i64 u0x16eeae54c7ebcc08, ; 117: System.Reflection.dll => 98
	i64 u0x17125c9a85b4929f, ; 118: lib_netstandard.dll.so => 167
	i64 u0x1716866f7416792e, ; 119: lib_System.Security.AccessControl.dll.so => 118
	i64 u0x174f71c46216e44a, ; 120: Xamarin.KotlinX.Coroutines.Core => 375
	i64 u0x1752c12f1e1fc00c, ; 121: System.Core => 21
	i64 u0x17b56e25558a5d36, ; 122: lib-hu-Microsoft.Maui.Controls.resources.dll.so => 393
	i64 u0x17f9358913beb16a, ; 123: System.Text.Encodings.Web => 137
	i64 u0x1809fb23f29ba44a, ; 124: lib_System.Reflection.TypeExtensions.dll.so => 97
	i64 u0x18402a709e357f3b, ; 125: lib_Xamarin.KotlinX.Serialization.Core.Jvm.dll.so => 379
	i64 u0x18a9befae51bb361, ; 126: System.Net.WebClient => 77
	i64 u0x18f0ce884e87d89a, ; 127: nb/Microsoft.Maui.Controls.resources.dll => 399
	i64 u0x18facb3695ca9224, ; 128: Refit.HttpClientFactory => 218
	i64 u0x193d7a04b7eda8bc, ; 129: lib_Xamarin.AndroidX.Print.dll.so => 283
	i64 u0x19777fba3c41b398, ; 130: Xamarin.AndroidX.Startup.StartupRuntime.dll => 291
	i64 u0x19a4c090f14ebb66, ; 131: System.Security.Claims => 119
	i64 u0x1a03fc927e6891b9, ; 132: Xamarin.GooglePlayServices.Measurement.Base => 352
	i64 u0x1a91866a319e9259, ; 133: lib_System.Collections.Concurrent.dll.so => 8
	i64 u0x1aac34d1917ba5d3, ; 134: lib_System.dll.so => 164
	i64 u0x1aad60783ffa3e5b, ; 135: lib-th-Microsoft.Maui.Controls.resources.dll.so => 408
	i64 u0x1aea8f1c3b282172, ; 136: lib_System.Net.Ping.dll.so => 70
	i64 u0x1b4b7a1d0d265fa2, ; 137: Xamarin.Android.Glide.DiskLruCache => 228
	i64 u0x1bbdb16cfa73e785, ; 138: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android => 272
	i64 u0x1bc766e07b2b4241, ; 139: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 286
	i64 u0x1bf5791bb4435277, ; 140: lib_Xamarin.GoogleAndroid.Annotations.dll.so => 344
	i64 u0x1c292b1598348d77, ; 141: Microsoft.Extensions.Diagnostics.dll => 187
	i64 u0x1c753b5ff15bce1b, ; 142: Mono.Android.Runtime.dll => 170
	i64 u0x1c7cbc0ecd18cdaf, ; 143: Xamarin.Firebase.Encoders.dll => 317
	i64 u0x1cd47467799d8250, ; 144: System.Threading.Tasks.dll => 144
	i64 u0x1d23eafdc6dc346c, ; 145: System.Globalization.Calendars.dll => 40
	i64 u0x1d33fb39b11be1fe, ; 146: Xamarin.CodeHaus.Mojo.AnimalSnifferAnnotations => 302
	i64 u0x1da4110562816681, ; 147: Xamarin.AndroidX.Security.SecurityCrypto.dll => 289
	i64 u0x1db6820994506bf5, ; 148: System.IO.FileSystem.AccessControl.dll => 47
	i64 u0x1dbb0c2c6a999acb, ; 149: System.Diagnostics.StackTrace => 30
	i64 u0x1dcda680b17dc5bb, ; 150: lib_Xamarin.Google.Guava.FailureAccess.dll.so => 342
	i64 u0x1e0486c615e0dc7e, ; 151: Xamarin.Firebase.Dynamic.Links => 316
	i64 u0x1e3d87657e9659bc, ; 152: Xamarin.AndroidX.Navigation.UI => 282
	i64 u0x1e71143913d56c10, ; 153: lib-ko-Microsoft.Maui.Controls.resources.dll.so => 397
	i64 u0x1e7c31185e2fb266, ; 154: lib_System.Threading.Tasks.Parallel.dll.so => 143
	i64 u0x1e8303b9cb0977f2, ; 155: Xamarin.Firebase.Functions.dll => 321
	i64 u0x1ed8fcce5e9b50a0, ; 156: Microsoft.Extensions.Options.dll => 194
	i64 u0x1f055d15d807e1b2, ; 157: System.Xml.XmlSerializer => 162
	i64 u0x1f1ed22c1085f044, ; 158: lib_System.Diagnostics.FileVersionInfo.dll.so => 28
	i64 u0x1f61df9c5b94d2c1, ; 159: lib_System.Numerics.dll.so => 84
	i64 u0x1f750bb5421397de, ; 160: lib_Xamarin.AndroidX.Tracing.Tracing.dll.so => 293
	i64 u0x1fc1a401201d7356, ; 161: Xamarin.Google.Dagger.dll => 339
	i64 u0x20237ea48006d7a8, ; 162: lib_System.Net.WebClient.dll.so => 77
	i64 u0x209375905fcc1bad, ; 163: lib_System.IO.Compression.Brotli.dll.so => 43
	i64 u0x20fab3cf2dfbc8df, ; 164: lib_System.Diagnostics.Process.dll.so => 29
	i64 u0x2110167c128cba15, ; 165: System.Globalization => 42
	i64 u0x21419508838f7547, ; 166: System.Runtime.CompilerServices.VisualC => 103
	i64 u0x2141deb83566de9d, ; 167: lib_Square.OkHttp3.dll.so => 220
	i64 u0x2174319c0d835bc9, ; 168: System.Runtime => 117
	i64 u0x218ae22aa3ec33e7, ; 169: Xamarin.Grpc.Protobuf.Lite.dll => 363
	i64 u0x2198e5bc8b7153fa, ; 170: Xamarin.AndroidX.Annotation.Experimental.dll => 233
	i64 u0x219ea1b751a4dee4, ; 171: lib_System.IO.Compression.ZipFile.dll.so => 45
	i64 u0x21cc7e445dcd5469, ; 172: System.Reflection.Emit.ILGeneration => 91
	i64 u0x220fd4f2e7c48170, ; 173: th/Microsoft.Maui.Controls.resources => 408
	i64 u0x224538d85ed15a82, ; 174: System.IO.Pipes => 56
	i64 u0x22908438c6bed1af, ; 175: lib_System.Threading.Timer.dll.so => 147
	i64 u0x22fbc14e981e3b45, ; 176: lib_Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll.so => 416
	i64 u0x2347c268e3e4e536, ; 177: Xamarin.GooglePlayServices.Basement.dll => 348
	i64 u0x237be844f1f812c7, ; 178: System.Threading.Thread.dll => 145
	i64 u0x23852b3bdc9f7096, ; 179: System.Resources.ResourceManager => 100
	i64 u0x23986dd7e5d4fc01, ; 180: System.IO.FileSystem.Primitives.dll => 49
	i64 u0x23bbd31edb1647bc, ; 181: lib_Plugin.Firebase.Core.dll.so => 210
	i64 u0x2407aef2bbe8fadf, ; 182: System.Console => 20
	i64 u0x240abe014b27e7d3, ; 183: Xamarin.AndroidX.Core.dll => 248
	i64 u0x247619fe4413f8bf, ; 184: System.Runtime.Serialization.Primitives.dll => 114
	i64 u0x24b87318591adabe, ; 185: lib_Xamarin.Firebase.Database.Collection.dll.so => 314
	i64 u0x24de8d301281575e, ; 186: Xamarin.Android.Glide => 226
	i64 u0x24e51ef140650238, ; 187: MvvmHelpers => 219
	i64 u0x252073cc3caa62c2, ; 188: fr/Microsoft.Maui.Controls.resources.dll => 389
	i64 u0x256b8d41255f01b1, ; 189: Xamarin.Google.Crypto.Tink.Android => 338
	i64 u0x25da9c898b4437a3, ; 190: Xamarin.Google.Android.Play.Integrity => 335
	i64 u0x25ffffb1789c4579, ; 191: Plugin.Firebase.Core => 210
	i64 u0x2662c629b96b0b30, ; 192: lib_Xamarin.Kotlin.StdLib.dll.so => 368
	i64 u0x268c1439f13bcc29, ; 193: lib_Microsoft.Extensions.Primitives.dll.so => 196
	i64 u0x26918e5f13c8fc7c, ; 194: Xamarin.Firebase.Firestore => 320
	i64 u0x26a670e154a9c54b, ; 195: System.Reflection.Extensions.dll => 94
	i64 u0x26d077d9678fe34f, ; 196: System.IO.dll => 58
	i64 u0x270a44600c921861, ; 197: System.IdentityModel.Tokens.Jwt => 223
	i64 u0x273f3515de5faf0d, ; 198: id/Microsoft.Maui.Controls.resources.dll => 394
	i64 u0x2742545f9094896d, ; 199: hr/Microsoft.Maui.Controls.resources => 392
	i64 u0x2759af78ab94d39b, ; 200: System.Net.WebSockets => 81
	i64 u0x27b2b16f3e9de038, ; 201: Xamarin.Google.Crypto.Tink.Android.dll => 338
	i64 u0x27b410442fad6cf1, ; 202: Java.Interop.dll => 168
	i64 u0x27b97e0d52c3034a, ; 203: System.Diagnostics.Debug => 26
	i64 u0x27c1227907352196, ; 204: lib_Xamarin.Firebase.Installations.InterOp.dll.so => 325
	i64 u0x27d88445c936a1af, ; 205: lib_Xamarin.Grpc.Android.dll.so => 358
	i64 u0x27eb21c6eb99d774, ; 206: Xamarin.Kotlin.StdLib.Jdk8.dll => 371
	i64 u0x2801845a2c71fbfb, ; 207: System.Net.Primitives.dll => 71
	i64 u0x28457a8c23b2a129, ; 208: Xamarin.Firebase.Iid => 322
	i64 u0x286835e259162700, ; 209: lib_Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll.so => 284
	i64 u0x288f0dc6b8b36b5f, ; 210: Refit.dll => 217
	i64 u0x28e52865585a1ebe, ; 211: Microsoft.Extensions.Diagnostics.Abstractions.dll => 188
	i64 u0x2949f3617a02c6b2, ; 212: Xamarin.AndroidX.ExifInterface => 258
	i64 u0x29f947844fb7fc11, ; 213: Microsoft.Maui.Controls.HotReload.Forms => 415
	i64 u0x2a128783efe70ba0, ; 214: uk/Microsoft.Maui.Controls.resources.dll => 410
	i64 u0x2a3b095612184159, ; 215: lib_System.Net.NetworkInformation.dll.so => 69
	i64 u0x2a6507a5ffabdf28, ; 216: System.Diagnostics.TraceSource.dll => 33
	i64 u0x2acd46d52b77f7fb, ; 217: lib_Plugin.Firebase.dll.so => 206
	i64 u0x2ad156c8e1354139, ; 218: fi/Microsoft.Maui.Controls.resources => 388
	i64 u0x2ad5d6b13b7a3e04, ; 219: System.ComponentModel.DataAnnotations.dll => 14
	i64 u0x2af298f63581d886, ; 220: System.Text.RegularExpressions.dll => 139
	i64 u0x2af615542f04da50, ; 221: System.IdentityModel.Tokens.Jwt.dll => 223
	i64 u0x2afc1c4f898552ee, ; 222: lib_System.Formats.Asn1.dll.so => 38
	i64 u0x2b148910ed40fbf9, ; 223: zh-Hant/Microsoft.Maui.Controls.resources.dll => 414
	i64 u0x2b6989d78cba9a15, ; 224: Xamarin.AndroidX.Concurrent.Futures.dll => 244
	i64 u0x2c517f906db0a191, ; 225: Xamarin.Google.Android.DataTransport.TransportBackendCct.dll => 332
	i64 u0x2c8bd14bb93a7d82, ; 226: lib-pl-Microsoft.Maui.Controls.resources.dll.so => 401
	i64 u0x2cbd9262ca785540, ; 227: lib_System.Text.Encoding.CodePages.dll.so => 134
	i64 u0x2cc9e1fed6257257, ; 228: lib_System.Reflection.Emit.Lightweight.dll.so => 92
	i64 u0x2cd723e9fe623c7c, ; 229: lib_System.Private.Xml.Linq.dll.so => 88
	i64 u0x2d169d318a968379, ; 230: System.Threading.dll => 148
	i64 u0x2d1d1413dd10c597, ; 231: Xamarin.Google.Guava.FailureAccess => 342
	i64 u0x2d47774b7d993f59, ; 232: sv/Microsoft.Maui.Controls.resources.dll => 407
	i64 u0x2d5ffcae1ad0aaca, ; 233: System.Data.dll => 24
	i64 u0x2d6267ac7de1d619, ; 234: Xamarin.Firebase.Database.Collection.dll => 314
	i64 u0x2d8414ebbf6218ce, ; 235: Plugin.Firebase.RemoteConfig.dll => 215
	i64 u0x2db915caf23548d2, ; 236: System.Text.Json.dll => 138
	i64 u0x2dcaa0bb15a4117a, ; 237: System.IO.UnmanagedMemoryStream.dll => 57
	i64 u0x2e5a40c319acb800, ; 238: System.IO.FileSystem => 51
	i64 u0x2e6f1f226821322a, ; 239: el/Microsoft.Maui.Controls.resources.dll => 386
	i64 u0x2e7ae36a08a1dbb3, ; 240: Xamarin.Firebase.Encoders => 317
	i64 u0x2e7c9658c7fb7927, ; 241: Microsoft.Extensions.Features.dll => 189
	i64 u0x2f02f94df3200fe5, ; 242: System.Diagnostics.Process => 29
	i64 u0x2f2e98e1c89b1aff, ; 243: System.Xml.ReaderWriter => 156
	i64 u0x2f5911d9ba814e4e, ; 244: System.Diagnostics.Tracing => 34
	i64 u0x2f84070a459bc31f, ; 245: lib_System.Xml.dll.so => 163
	i64 u0x2fd92a71c7638cfd, ; 246: Xamarin.Firebase.Database.Collection => 314
	i64 u0x2ff49de6a71764a1, ; 247: lib_Microsoft.Extensions.Http.dll.so => 190
	i64 u0x301713b5054e4388, ; 248: Xamarin.GooglePlayServices.Stats.dll => 356
	i64 u0x3053de11b4c0cb63, ; 249: lib_Xamarin.Firebase.Measurement.Connector.dll.so => 326
	i64 u0x30829702a4057a12, ; 250: Xamarin.Google.Android.DataTransport.TransportBackendCct => 332
	i64 u0x309ee9eeec09a71e, ; 251: lib_Xamarin.AndroidX.Fragment.dll.so => 259
	i64 u0x309f2bedefa9a318, ; 252: Microsoft.IdentityModel.Abstractions => 197
	i64 u0x30c6dda129408828, ; 253: System.IO.IsolatedStorage => 52
	i64 u0x31195fef5d8fb552, ; 254: _Microsoft.Android.Resource.Designer.dll => 419
	i64 u0x312c8ed623cbfc8d, ; 255: Xamarin.AndroidX.Window.dll => 300
	i64 u0x31496b779ed0663d, ; 256: lib_System.Reflection.DispatchProxy.dll.so => 90
	i64 u0x31f946282b3de50c, ; 257: Xamarin.Firebase.Datatransport.dll => 315
	i64 u0x3200b1eae7efaded, ; 258: lib_Xamarin.Firebase.Encoders.JSON.dll.so => 318
	i64 u0x32243413e774362a, ; 259: Xamarin.AndroidX.CardView.dll => 240
	i64 u0x3235427f8d12dae1, ; 260: lib_System.Drawing.Primitives.dll.so => 35
	i64 u0x329753a17a517811, ; 261: fr/Microsoft.Maui.Controls.resources => 389
	i64 u0x32aa989ff07a84ff, ; 262: lib_System.Xml.ReaderWriter.dll.so => 156
	i64 u0x33829542f112d59b, ; 263: System.Collections.Immutable => 9
	i64 u0x33a31443733849fe, ; 264: lib-es-Microsoft.Maui.Controls.resources.dll.so => 387
	i64 u0x341abc357fbb4ebf, ; 265: lib_System.Net.Sockets.dll.so => 76
	i64 u0x342397b849d48e49, ; 266: Xamarin.Grpc.Core => 361
	i64 u0x3496c1e2dcaf5ecc, ; 267: lib_System.IO.Pipes.AccessControl.dll.so => 55
	i64 u0x34dfd74fe2afcf37, ; 268: Microsoft.Maui => 203
	i64 u0x34e292762d9615df, ; 269: cs/Microsoft.Maui.Controls.resources.dll => 383
	i64 u0x34f550615634aa64, ; 270: lib_Xamarin.Firebase.Encoders.dll.so => 317
	i64 u0x3508234247f48404, ; 271: Microsoft.Maui.Controls => 201
	i64 u0x353590da528c9d22, ; 272: System.ComponentModel.Annotations => 13
	i64 u0x353c74869339570c, ; 273: lib_Xamarin.Firebase.Auth.dll.so => 307
	i64 u0x3549870798b4cd30, ; 274: lib_Xamarin.AndroidX.ViewPager2.dll.so => 299
	i64 u0x355282fc1c909694, ; 275: Microsoft.Extensions.Configuration => 182
	i64 u0x3552fc5d578f0fbf, ; 276: Xamarin.AndroidX.Arch.Core.Common => 237
	i64 u0x355c649948d55d97, ; 277: lib_System.Runtime.Intrinsics.dll.so => 109
	i64 u0x356fd122ba041cb4, ; 278: lib_Xamarin.Grpc.Protobuf.Lite.dll.so => 363
	i64 u0x35ea9d1c6834bc8c, ; 279: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 275
	i64 u0x3628ab68db23a01a, ; 280: lib_System.Diagnostics.Tools.dll.so => 32
	i64 u0x364703ab05867b92, ; 281: Xamarin.Firebase.Components => 311
	i64 u0x3673b042508f5b6b, ; 282: lib_System.Runtime.Extensions.dll.so => 104
	i64 u0x36740f1a8ecdc6c4, ; 283: System.Numerics => 84
	i64 u0x36b2b50fdf589ae2, ; 284: System.Reflection.Emit.Lightweight => 92
	i64 u0x36cada77dc79928b, ; 285: System.IO.MemoryMappedFiles => 53
	i64 u0x374ef46b06791af6, ; 286: System.Reflection.Primitives.dll => 96
	i64 u0x376bf93e521a5417, ; 287: lib_Xamarin.Jetbrains.Annotations.dll.so => 367
	i64 u0x37bc29f3183003b6, ; 288: lib_System.IO.dll.so => 58
	i64 u0x380134e03b1e160a, ; 289: System.Collections.Immutable.dll => 9
	i64 u0x38049b5c59b39324, ; 290: System.Runtime.CompilerServices.Unsafe => 102
	i64 u0x385c17636bb6fe6e, ; 291: Xamarin.AndroidX.CustomView.dll => 251
	i64 u0x38869c811d74050e, ; 292: System.Net.NameResolution.dll => 68
	i64 u0x393c226616977fdb, ; 293: lib_Xamarin.AndroidX.ViewPager.dll.so => 298
	i64 u0x395e37c3334cf82a, ; 294: lib-ca-Microsoft.Maui.Controls.resources.dll.so => 382
	i64 u0x3ab5859054645f72, ; 295: System.Security.Cryptography.Primitives.dll => 125
	i64 u0x3ad75090c3fac0e9, ; 296: lib_Xamarin.AndroidX.ResourceInspection.Annotation.dll.so => 286
	i64 u0x3ae44ac43a1fbdbb, ; 297: System.Runtime.Serialization => 116
	i64 u0x3b860f9932505633, ; 298: lib_System.Text.Encoding.Extensions.dll.so => 135
	i64 u0x3bea9ebe8c027c01, ; 299: lib_Microsoft.IdentityModel.Tokens.dll.so => 200
	i64 u0x3c0c1b892a2cb57e, ; 300: Plugin.Firebase.CloudMessaging => 209
	i64 u0x3c3aafb6b3a00bf6, ; 301: lib_System.Security.Cryptography.X509Certificates.dll.so => 126
	i64 u0x3c4049146b59aa90, ; 302: System.Runtime.InteropServices.JavaScript => 106
	i64 u0x3c7c495f58ac5ee9, ; 303: Xamarin.Kotlin.StdLib => 368
	i64 u0x3c7e5ed3d5db71bb, ; 304: System.Security => 131
	i64 u0x3c90a7b70f45292a, ; 305: Xamarin.Grpc.OkHttp.dll => 362
	i64 u0x3cc1676a8781bdbc, ; 306: Xamarin.Firebase.Auth.Interop.dll => 308
	i64 u0x3cd9d281d402eb9b, ; 307: Xamarin.AndroidX.Browser.dll => 239
	i64 u0x3d1c50cc001a991e, ; 308: Xamarin.Google.Guava.ListenableFuture.dll => 343
	i64 u0x3d2b1913edfc08d7, ; 309: lib_System.Threading.ThreadPool.dll.so => 146
	i64 u0x3d46f0b995082740, ; 310: System.Xml.Linq => 155
	i64 u0x3d8a8f400514a790, ; 311: Xamarin.AndroidX.Fragment.Ktx.dll => 260
	i64 u0x3d9c2a242b040a50, ; 312: lib_Xamarin.AndroidX.Core.dll.so => 248
	i64 u0x3dbb6b9f5ab90fa7, ; 313: lib_Xamarin.AndroidX.DynamicAnimation.dll.so => 255
	i64 u0x3e250296f3b5714d, ; 314: Xamarin.Firebase.Measurement.Connector => 326
	i64 u0x3e5441657549b213, ; 315: Xamarin.AndroidX.ResourceInspection.Annotation => 286
	i64 u0x3e57d4d195c53c2e, ; 316: System.Reflection.TypeExtensions => 97
	i64 u0x3e616ab4ed1f3f15, ; 317: lib_System.Data.dll.so => 24
	i64 u0x3f1d226e6e06db7e, ; 318: Xamarin.AndroidX.SlidingPaneLayout.dll => 290
	i64 u0x3f510adf788828dd, ; 319: System.Threading.Tasks.Extensions => 142
	i64 u0x407740ff2e914d86, ; 320: Xamarin.AndroidX.Print.dll => 283
	i64 u0x407a10bb4bf95829, ; 321: lib_Xamarin.AndroidX.Navigation.Common.dll.so => 279
	i64 u0x40c98b6bd77346d4, ; 322: Microsoft.VisualBasic.dll => 3
	i64 u0x41833cf766d27d96, ; 323: mscorlib => 166
	i64 u0x41cab042be111c34, ; 324: lib_Xamarin.AndroidX.AppCompat.AppCompatResources.dll.so => 236
	i64 u0x41f64d46793f6584, ; 325: Plugin.Firebase.Analytics.dll => 207
	i64 u0x423a9ecc4d905a88, ; 326: lib_System.Resources.ResourceManager.dll.so => 100
	i64 u0x423bf51ae7def810, ; 327: System.Xml.XPath => 160
	i64 u0x42462ff15ddba223, ; 328: System.Resources.Reader.dll => 99
	i64 u0x42a31b86e6ccc3f0, ; 329: System.Diagnostics.Contracts => 25
	i64 u0x430e95b891249788, ; 330: lib_System.Reflection.Emit.dll.so => 93
	i64 u0x43375950ec7c1b6a, ; 331: netstandard.dll => 167
	i64 u0x434c4e1d9284cdae, ; 332: Mono.Android.dll => 171
	i64 u0x43505013578652a0, ; 333: lib_Xamarin.AndroidX.Activity.Ktx.dll.so => 231
	i64 u0x437d06c381ed575a, ; 334: lib_Microsoft.VisualBasic.dll.so => 3
	i64 u0x43950f84de7cc79a, ; 335: pl/Microsoft.Maui.Controls.resources.dll => 401
	i64 u0x43e8ca5bc927ff37, ; 336: lib_Xamarin.AndroidX.Emoji2.ViewsHelper.dll.so => 257
	i64 u0x447e266c182176d4, ; 337: Xamarin.GooglePlayServices.Measurement.Api => 351
	i64 u0x448bd33429269b19, ; 338: Microsoft.CSharp => 1
	i64 u0x4499fa3c8e494654, ; 339: lib_System.Runtime.Serialization.Primitives.dll.so => 114
	i64 u0x4515080865a951a5, ; 340: Xamarin.Kotlin.StdLib.dll => 368
	i64 u0x4545802489b736b9, ; 341: Xamarin.AndroidX.Fragment.Ktx => 260
	i64 u0x454b4d1e66bb783c, ; 342: Xamarin.AndroidX.Lifecycle.Process => 268
	i64 u0x458d2df79ac57c1d, ; 343: lib_System.IdentityModel.Tokens.Jwt.dll.so => 223
	i64 u0x45c40276a42e283e, ; 344: System.Diagnostics.TraceSource => 33
	i64 u0x45d443f2a29adc37, ; 345: System.AppContext.dll => 6
	i64 u0x4688df2d3127fb7a, ; 346: lib_Xamarin.GooglePlayServices.Measurement.dll.so => 350
	i64 u0x46a4213bc97fe5ae, ; 347: lib-ru-Microsoft.Maui.Controls.resources.dll.so => 405
	i64 u0x46e11054e74afdb7, ; 348: Xamarin.Firebase.AppCheck.Interop => 306
	i64 u0x47358bd471172e1d, ; 349: lib_System.Xml.Linq.dll.so => 155
	i64 u0x47a2af602ae797ed, ; 350: lib_Xamarin.KotlinX.Coroutines.Play.Services.dll.so => 377
	i64 u0x47daf4e1afbada10, ; 351: pt/Microsoft.Maui.Controls.resources => 403
	i64 u0x480c0a47dd42dd81, ; 352: lib_System.IO.MemoryMappedFiles.dll.so => 53
	i64 u0x488d293220a4fe37, ; 353: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 262
	i64 u0x48a6d2fa2eb5d049, ; 354: Microsoft.AspNetCore.SignalR.Protocols.Json => 181
	i64 u0x49946f0d78a813ad, ; 355: Xamarin.GooglePlayServices.Auth.Api.Phone => 346
	i64 u0x49e952f19a4e2022, ; 356: System.ObjectModel => 85
	i64 u0x49f6ab815e178ca9, ; 357: lib_Xamarin.Firebase.Common.dll.so => 309
	i64 u0x49f9e6948a8131e4, ; 358: lib_Xamarin.AndroidX.VersionedParcelable.dll.so => 297
	i64 u0x4a5667b2462a664b, ; 359: lib_Xamarin.AndroidX.Navigation.UI.dll.so => 282
	i64 u0x4a7a18981dbd56bc, ; 360: System.IO.Compression.FileSystem.dll => 44
	i64 u0x4aa5c60350917c06, ; 361: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll.so => 267
	i64 u0x4b07a0ed0ab33ff4, ; 362: System.Runtime.Extensions.dll => 104
	i64 u0x4b576d47ac054f3c, ; 363: System.IO.FileSystem.AccessControl => 47
	i64 u0x4b7b6532ded934b7, ; 364: System.Text.Json => 138
	i64 u0x4bb6f6e34164a378, ; 365: Square.OkIO.JVM.dll => 222
	i64 u0x4c7755cf07ad2d5f, ; 366: System.Net.Http.Json.dll => 64
	i64 u0x4c915fc509d9e8d7, ; 367: Xamarin.GooglePlayServices.Measurement.Sdk.Api => 355
	i64 u0x4cad38a03a928144, ; 368: lib_Xamarin.Firebase.Encoders.Proto.dll.so => 319
	i64 u0x4cc5f15266470798, ; 369: lib_Xamarin.AndroidX.Loader.dll.so => 277
	i64 u0x4cf6f67dc77aacd2, ; 370: System.Net.NetworkInformation.dll => 69
	i64 u0x4d3183dd245425d4, ; 371: System.Net.WebSockets.Client.dll => 80
	i64 u0x4d479f968a05e504, ; 372: System.Linq.Expressions.dll => 59
	i64 u0x4d55a010ffc4faff, ; 373: System.Private.Xml => 89
	i64 u0x4d5cbe77561c5b2e, ; 374: System.Web.dll => 153
	i64 u0x4d77512dbd86ee4c, ; 375: lib_Xamarin.AndroidX.Arch.Core.Common.dll.so => 237
	i64 u0x4d7793536e79c309, ; 376: System.ServiceProcess => 133
	i64 u0x4d95fccc1f67c7ca, ; 377: System.Runtime.Loader.dll => 110
	i64 u0x4dcf44c3c9b076a2, ; 378: it/Microsoft.Maui.Controls.resources.dll => 395
	i64 u0x4dd9247f1d2c3235, ; 379: Xamarin.AndroidX.Loader.dll => 277
	i64 u0x4e2aeee78e2c4a87, ; 380: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 284
	i64 u0x4e32f00cb0937401, ; 381: Mono.Android.Runtime => 170
	i64 u0x4e39d45ce072e04b, ; 382: Microsoft.AspNetCore.SignalR.Common.dll => 180
	i64 u0x4e46048835331fcd, ; 383: Xamarin.Firebase.Analytics => 304
	i64 u0x4e5eea4668ac2b18, ; 384: System.Text.Encoding.CodePages => 134
	i64 u0x4e742b34f53425d7, ; 385: Xamarin.Firebase.Encoders.Proto.dll => 319
	i64 u0x4ebd0c4b82c5eefc, ; 386: lib_System.Threading.Channels.dll.so => 225
	i64 u0x4ee8eaa9c9c1151a, ; 387: System.Globalization.Calendars => 40
	i64 u0x4f21ee6ef9eb527e, ; 388: ca/Microsoft.Maui.Controls.resources => 382
	i64 u0x4f805f1e120ed022, ; 389: Xamarin.GooglePlayServices.Ads.Identifier => 345
	i64 u0x4fdc964ec1888e25, ; 390: lib_Microsoft.Extensions.Configuration.Binder.dll.so => 184
	i64 u0x4ff8ea8951a69b9f, ; 391: Xamarin.Grpc.Android.dll => 358
	i64 u0x4ffd65baff757598, ; 392: Microsoft.IdentityModel.Tokens => 200
	i64 u0x5037f0be3c28c7a3, ; 393: lib_Microsoft.Maui.Controls.dll.so => 201
	i64 u0x50bd8cfae0f07500, ; 394: lib_Xamarin.GooglePlayServices.Stats.dll.so => 356
	i64 u0x50c3a29b21050d45, ; 395: System.Linq.Parallel.dll => 60
	i64 u0x5112ed116d87baf8, ; 396: CommunityToolkit.Mvvm => 173
	i64 u0x5116b21580ae6eb0, ; 397: Microsoft.Extensions.Configuration.Binder.dll => 184
	i64 u0x5131bbe80989093f, ; 398: Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll => 274
	i64 u0x515d61d6527dac70, ; 399: lib_Xamarin.Firebase.Auth.Interop.dll.so => 308
	i64 u0x516324a5050a7e3c, ; 400: System.Net.WebProxy => 79
	i64 u0x516d6f0b21a303de, ; 401: lib_System.Diagnostics.Contracts.dll.so => 25
	i64 u0x51bb8a2afe774e32, ; 402: System.Drawing => 36
	i64 u0x51d5fd31c5909faf, ; 403: lib_Xamarin.Google.Android.Recaptcha.dll.so => 336
	i64 u0x5247c5c32a4140f0, ; 404: System.Resources.Reader => 99
	i64 u0x526bb15e3c386364, ; 405: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 271
	i64 u0x526ce79eb8e90527, ; 406: lib_System.Net.Primitives.dll.so => 71
	i64 u0x52829f00b4467c38, ; 407: lib_System.Data.Common.dll.so => 22
	i64 u0x529ffe06f39ab8db, ; 408: Xamarin.AndroidX.Core => 248
	i64 u0x52ff996554dbf352, ; 409: Microsoft.Maui.Graphics => 205
	i64 u0x535f7e40e8fef8af, ; 410: lib-sk-Microsoft.Maui.Controls.resources.dll.so => 406
	i64 u0x536bf0b25fe5ae63, ; 411: Plugin.Firebase.DynamicLinks => 212
	i64 u0x53978aac584c666e, ; 412: lib_System.Security.Cryptography.Cng.dll.so => 121
	i64 u0x53a96d5c86c9e194, ; 413: System.Net.NetworkInformation => 69
	i64 u0x53be1038a61e8d44, ; 414: System.Runtime.InteropServices.RuntimeInformation.dll => 107
	i64 u0x53c3014b9437e684, ; 415: lib-zh-HK-Microsoft.Maui.Controls.resources.dll.so => 412
	i64 u0x53e450ebd586f842, ; 416: lib_Xamarin.AndroidX.LocalBroadcastManager.dll.so => 278
	i64 u0x5435e6f049e9bc37, ; 417: System.Security.Claims.dll => 119
	i64 u0x54795225dd1587af, ; 418: lib_System.Runtime.dll.so => 117
	i64 u0x547a34f14e5f6210, ; 419: Xamarin.AndroidX.Lifecycle.Common.dll => 263
	i64 u0x54e0759a7e57a36e, ; 420: lib_MvvmHelpers.dll.so => 219
	i64 u0x556e8b63b660ab8b, ; 421: Xamarin.AndroidX.Lifecycle.Common.Jvm.dll => 264
	i64 u0x5588627c9a108ec9, ; 422: System.Collections.Specialized => 11
	i64 u0x55a898e4f42e3fae, ; 423: Microsoft.VisualBasic.Core.dll => 2
	i64 u0x55fa0c610fe93bb1, ; 424: lib_System.Security.Cryptography.OpenSsl.dll.so => 124
	i64 u0x56442b99bc64bb47, ; 425: System.Runtime.Serialization.Xml.dll => 115
	i64 u0x56a5d2c17db41bcb, ; 426: Xamarin.Google.Android.DataTransport.TransportRuntime => 333
	i64 u0x56a8b26e1aeae27b, ; 427: System.Threading.Tasks.Dataflow => 141
	i64 u0x56eacd1e7e04c04e, ; 428: lib_Xamarin.Google.Android.Play.Integrity.dll.so => 335
	i64 u0x56f932d61e93c07f, ; 429: System.Globalization.Extensions => 41
	i64 u0x571c5cfbec5ae8e2, ; 430: System.Private.Uri => 87
	i64 u0x57201164aeb974e3, ; 431: Xamarin.Google.Guava.FailureAccess.dll => 342
	i64 u0x5722aed33ba95e06, ; 432: lib_Xamarin.GooglePlayServices.Measurement.Impl.dll.so => 353
	i64 u0x576499c9f52fea31, ; 433: Xamarin.AndroidX.Annotation => 232
	i64 u0x579a06fed6eec900, ; 434: System.Private.CoreLib.dll => 172
	i64 u0x57c542c14049b66d, ; 435: System.Diagnostics.DiagnosticSource => 27
	i64 u0x581a8bd5cfda563e, ; 436: System.Threading.Timer => 147
	i64 u0x584ac38e21d2fde1, ; 437: Microsoft.Extensions.Configuration.Binder => 184
	i64 u0x58601b2dda4a27b9, ; 438: lib-ja-Microsoft.Maui.Controls.resources.dll.so => 396
	i64 u0x58688d9af496b168, ; 439: Microsoft.Extensions.DependencyInjection.dll => 185
	i64 u0x588c167a79db6bfb, ; 440: lib_Xamarin.Google.ErrorProne.Annotations.dll.so => 340
	i64 u0x5906028ae5151104, ; 441: Xamarin.AndroidX.Activity.Ktx => 231
	i64 u0x595a356d23e8da9a, ; 442: lib_Microsoft.CSharp.dll.so => 1
	i64 u0x59bf5951f09e7573, ; 443: Plugin.Firebase.Functions.dll => 214
	i64 u0x59f9e60b9475085f, ; 444: lib_Xamarin.AndroidX.Annotation.Experimental.dll.so => 233
	i64 u0x5a6dc081b000c5d7, ; 445: lib_Xamarin.Grpc.OkHttp.dll.so => 362
	i64 u0x5a745f5101a75527, ; 446: lib_System.IO.Compression.FileSystem.dll.so => 44
	i64 u0x5a89a886ae30258d, ; 447: lib_Xamarin.AndroidX.CoordinatorLayout.dll.so => 247
	i64 u0x5a8f6699f4a1caa9, ; 448: lib_System.Threading.dll.so => 148
	i64 u0x5ae8e4f3eae4d547, ; 449: Xamarin.AndroidX.Legacy.Support.Core.Utils => 262
	i64 u0x5ae9cd33b15841bf, ; 450: System.ComponentModel => 18
	i64 u0x5aeb8cd498d4823e, ; 451: lib_Xamarin.Google.Guava.dll.so => 341
	i64 u0x5b00c69e1da8fac1, ; 452: Plugin.Firebase.RemoteConfig => 215
	i64 u0x5b247cf480c75903, ; 453: Microsoft.AspNetCore.Http.Connections.Common.dll => 177
	i64 u0x5b54391bdc6fcfe6, ; 454: System.Private.DataContractSerialization => 86
	i64 u0x5b5f0e240a06a2a2, ; 455: da/Microsoft.Maui.Controls.resources.dll => 384
	i64 u0x5b755276902c8414, ; 456: Xamarin.GooglePlayServices.Base => 347
	i64 u0x5b8109e8e14c5e3e, ; 457: System.Globalization.Extensions.dll => 41
	i64 u0x5bddd04d72a9e350, ; 458: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 267
	i64 u0x5bdf16b09da116ab, ; 459: Xamarin.AndroidX.Collection => 241
	i64 u0x5bff6a70194300bd, ; 460: lib_Xamarin.Kotlin.StdLib.Jdk8.dll.so => 371
	i64 u0x5c019d5266093159, ; 461: lib_Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android.dll.so => 272
	i64 u0x5c294d94f201783b, ; 462: lib_Microsoft.AspNetCore.Http.Connections.Client.dll.so => 176
	i64 u0x5c30a4a35f9cc8c4, ; 463: lib_System.Reflection.Extensions.dll.so => 94
	i64 u0x5c393624b8176517, ; 464: lib_Microsoft.Extensions.Logging.dll.so => 191
	i64 u0x5c53c29f5073b0c9, ; 465: System.Diagnostics.FileVersionInfo => 28
	i64 u0x5c87463c575c7616, ; 466: lib_System.Globalization.Extensions.dll.so => 41
	i64 u0x5d0a4a29b02d9d3c, ; 467: System.Net.WebHeaderCollection.dll => 78
	i64 u0x5d40c9b15181641f, ; 468: lib_Xamarin.AndroidX.Emoji2.dll.so => 256
	i64 u0x5d6ca10d35e9485b, ; 469: lib_Xamarin.AndroidX.Concurrent.Futures.dll.so => 244
	i64 u0x5d7ec76c1c703055, ; 470: System.Threading.Tasks.Parallel => 143
	i64 u0x5db0cbbd1028510e, ; 471: lib_System.Runtime.InteropServices.dll.so => 108
	i64 u0x5db30905d3e5013b, ; 472: Xamarin.AndroidX.Collection.Jvm.dll => 242
	i64 u0x5dd10b96620cce23, ; 473: Xamarin.Firebase.Iid.Interop => 323
	i64 u0x5e467bc8f09ad026, ; 474: System.Collections.Specialized.dll => 11
	i64 u0x5e5173b3208d97e7, ; 475: System.Runtime.Handles.dll => 105
	i64 u0x5e866d69770119c9, ; 476: lib_Xamarin.Firebase.Abt.dll.so => 303
	i64 u0x5ea92fdb19ec8c4c, ; 477: System.Text.Encodings.Web.dll => 137
	i64 u0x5eb8046dd40e9ac3, ; 478: System.ComponentModel.Primitives => 16
	i64 u0x5ec272d219c9aba4, ; 479: System.Security.Cryptography.Csp.dll => 122
	i64 u0x5eee1376d94c7f5e, ; 480: System.Net.HttpListener.dll => 66
	i64 u0x5f36ccf5c6a57e24, ; 481: System.Xml.ReaderWriter.dll => 156
	i64 u0x5f4294b9b63cb842, ; 482: System.Data.Common => 22
	i64 u0x5f4fa8b9ffd0e2c5, ; 483: lib_Xamarin.Google.Android.DataTransport.TransportApi.dll.so => 331
	i64 u0x5f9a2d823f664957, ; 484: lib-el-Microsoft.Maui.Controls.resources.dll.so => 386
	i64 u0x5fa6da9c3cd8142a, ; 485: lib_Xamarin.KotlinX.Serialization.Core.dll.so => 378
	i64 u0x5fac98e0b37a5b9d, ; 486: System.Runtime.CompilerServices.Unsafe.dll => 102
	i64 u0x609f4b7b63d802d4, ; 487: lib_Microsoft.Extensions.DependencyInjection.dll.so => 185
	i64 u0x60cd4e33d7e60134, ; 488: Xamarin.KotlinX.Coroutines.Core.Jvm => 376
	i64 u0x60ee72f71c4b3113, ; 489: Xamarin.Firebase.Iid.Interop.dll => 323
	i64 u0x60f62d786afcf130, ; 490: System.Memory => 63
	i64 u0x61bb78c89f867353, ; 491: System.IO => 58
	i64 u0x61be8d1299194243, ; 492: Microsoft.Maui.Controls.Xaml => 202
	i64 u0x61d2cba29557038f, ; 493: de/Microsoft.Maui.Controls.resources => 385
	i64 u0x61d88f399afb2f45, ; 494: lib_System.Runtime.Loader.dll.so => 110
	i64 u0x622eef6f9e59068d, ; 495: System.Private.CoreLib => 172
	i64 u0x62e976fd765a2339, ; 496: Xamarin.Firebase.Auth.Interop => 308
	i64 u0x63982c87366f9be8, ; 497: Xamarin.Google.Guava => 341
	i64 u0x63cdbd66ac39bb46, ; 498: lib_Microsoft.VisualStudio.DesignTools.XamlTapContract.dll.so => 418
	i64 u0x63d5e3aa4ef9b931, ; 499: Xamarin.KotlinX.Coroutines.Android.dll => 374
	i64 u0x63f1f6883c1e23c2, ; 500: lib_System.Collections.Immutable.dll.so => 9
	i64 u0x6400f68068c1e9f1, ; 501: Xamarin.Google.Android.Material.dll => 334
	i64 u0x640e3b14dbd325c2, ; 502: System.Security.Cryptography.Algorithms.dll => 120
	i64 u0x64587004560099b9, ; 503: System.Reflection => 98
	i64 u0x646bbda4b2219cc2, ; 504: lib_Xamarin.Firebase.Datatransport.dll.so => 315
	i64 u0x64b1529a438a3c45, ; 505: lib_System.Runtime.Handles.dll.so => 105
	i64 u0x64b61dd9da8a4d57, ; 506: System.Net.ServerSentEvents.dll => 224
	i64 u0x64f30e567cb41fac, ; 507: Xamarin.KotlinX.Coroutines.Play.Services => 377
	i64 u0x6565fba2cd8f235b, ; 508: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 275
	i64 u0x659dc45417570048, ; 509: Refit => 217
	i64 u0x65ecac39144dd3cc, ; 510: Microsoft.Maui.Controls.dll => 201
	i64 u0x65ece51227bfa724, ; 511: lib_System.Runtime.Numerics.dll.so => 111
	i64 u0x661722438787b57f, ; 512: Xamarin.AndroidX.Annotation.Jvm.dll => 234
	i64 u0x6679b2337ee6b22a, ; 513: lib_System.IO.FileSystem.Primitives.dll.so => 49
	i64 u0x6692e924eade1b29, ; 514: lib_System.Console.dll.so => 20
	i64 u0x669b2e9020ef2b4e, ; 515: Xamarin.Firebase.Datatransport => 315
	i64 u0x66a4e5c6a3fb0bae, ; 516: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll.so => 274
	i64 u0x66d13304ce1a3efa, ; 517: Xamarin.AndroidX.CursorAdapter => 250
	i64 u0x672a10d319608935, ; 518: lib_Microsoft.AspNetCore.Http.Connections.Common.dll.so => 177
	i64 u0x674303f65d8fad6f, ; 519: lib_System.Net.Quic.dll.so => 72
	i64 u0x6756ca4cad62e9d6, ; 520: lib_Xamarin.AndroidX.ConstraintLayout.Core.dll.so => 246
	i64 u0x67bca11acb828d8d, ; 521: lib_Xamarin.Google.Android.DataTransport.TransportBackendCct.dll.so => 332
	i64 u0x67c0802770244408, ; 522: System.Windows.dll => 154
	i64 u0x68100b69286e27cd, ; 523: lib_System.Formats.Tar.dll.so => 39
	i64 u0x68558ec653afa616, ; 524: lib-da-Microsoft.Maui.Controls.resources.dll.so => 384
	i64 u0x6872ec7a2e36b1ac, ; 525: System.Drawing.Primitives.dll => 35
	i64 u0x68bb2c417aa9b61c, ; 526: Xamarin.KotlinX.AtomicFU.dll => 372
	i64 u0x68fbbbe2eb455198, ; 527: System.Formats.Asn1 => 38
	i64 u0x69063fc0ba8e6bdd, ; 528: he/Microsoft.Maui.Controls.resources.dll => 390
	i64 u0x69a3e26c76f6eec4, ; 529: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 301
	i64 u0x6a4d7577b2317255, ; 530: System.Runtime.InteropServices.dll => 108
	i64 u0x6a61e5b7d5907416, ; 531: lib_Xamarin.GooglePlayServices.Auth.Api.Phone.dll.so => 346
	i64 u0x6a83dcc5e7641aa4, ; 532: lib_Xamarin.GooglePlayServices.Measurement.Api.dll.so => 351
	i64 u0x6ace3b74b15ee4a4, ; 533: nb/Microsoft.Maui.Controls.resources => 399
	i64 u0x6afcedb171067e2b, ; 534: System.Core.dll => 21
	i64 u0x6bbbec44779e48c2, ; 535: Xamarin.GooglePlayServices.Measurement.Base.dll => 352
	i64 u0x6be5fa9d8f68118d, ; 536: Square.OkHttp3.dll => 220
	i64 u0x6bef98e124147c24, ; 537: Xamarin.Jetbrains.Annotations => 367
	i64 u0x6ce874bff138ce2b, ; 538: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 273
	i64 u0x6cfa2bd175637715, ; 539: Square.OkIO.JVM => 222
	i64 u0x6d12bfaa99c72b1f, ; 540: lib_Microsoft.Maui.Graphics.dll.so => 205
	i64 u0x6d70755158ca866e, ; 541: lib_System.ComponentModel.EventBasedAsync.dll.so => 15
	i64 u0x6d79993361e10ef2, ; 542: Microsoft.Extensions.Primitives => 196
	i64 u0x6d7eeca99577fc8b, ; 543: lib_System.Net.WebProxy.dll.so => 79
	i64 u0x6d8515b19946b6a2, ; 544: System.Net.WebProxy.dll => 79
	i64 u0x6d86d56b84c8eb71, ; 545: lib_Xamarin.AndroidX.CursorAdapter.dll.so => 250
	i64 u0x6d9bea6b3e895cf7, ; 546: Microsoft.Extensions.Primitives.dll => 196
	i64 u0x6dd9bf4083de3f6a, ; 547: Xamarin.AndroidX.DocumentFile.dll => 253
	i64 u0x6e25a02c3833319a, ; 548: lib_Xamarin.AndroidX.Navigation.Fragment.dll.so => 280
	i64 u0x6e79c6bd8627412a, ; 549: Xamarin.AndroidX.SavedState.SavedState.Ktx => 288
	i64 u0x6e838d9a2a6f6c9e, ; 550: lib_System.ValueTuple.dll.so => 151
	i64 u0x6e9965ce1095e60a, ; 551: lib_System.Core.dll.so => 21
	i64 u0x6f304d1dec1019e9, ; 552: lib_Xamarin.Firebase.Installations.dll.so => 324
	i64 u0x6fd2265da78b93a4, ; 553: lib_Microsoft.Maui.dll.so => 203
	i64 u0x6fdfc7de82c33008, ; 554: cs/Microsoft.Maui.Controls.resources => 383
	i64 u0x6ffc4967cc47ba57, ; 555: System.IO.FileSystem.Watcher.dll => 50
	i64 u0x701cd46a1c25a5fe, ; 556: System.IO.FileSystem.dll => 51
	i64 u0x70664ad3307f4fbf, ; 557: Xamarin.Grpc.Core.dll => 361
	i64 u0x70c1154d9ce7bd51, ; 558: Xamarin.Kotlin.StdLib.Common.dll => 369
	i64 u0x70e99f48c05cb921, ; 559: tr/Microsoft.Maui.Controls.resources.dll => 409
	i64 u0x70fd3deda22442d2, ; 560: lib-nb-Microsoft.Maui.Controls.resources.dll.so => 399
	i64 u0x71485e7ffdb4b958, ; 561: System.Reflection.Extensions => 94
	i64 u0x7162a2fce67a945f, ; 562: lib_Xamarin.Android.Glide.Annotations.dll.so => 227
	i64 u0x717530326f808838, ; 563: lib_Microsoft.Extensions.Diagnostics.Abstractions.dll.so => 188
	i64 u0x719176ec28437f1c, ; 564: Xamarin.Protobuf.JavaLite => 380
	i64 u0x71a495ea3761dde8, ; 565: lib-it-Microsoft.Maui.Controls.resources.dll.so => 395
	i64 u0x71ad672adbe48f35, ; 566: System.ComponentModel.Primitives.dll => 16
	i64 u0x7207f5c4519b0c98, ; 567: Plugin.Firebase.Auth.dll => 208
	i64 u0x7242820f67bc4ad6, ; 568: Microsoft.AspNetCore.SignalR.Common => 180
	i64 u0x725f5a9e82a45c81, ; 569: System.Security.Cryptography.Encoding => 123
	i64 u0x72b1fb4109e08d7b, ; 570: lib-hr-Microsoft.Maui.Controls.resources.dll.so => 392
	i64 u0x72e0300099accce1, ; 571: System.Xml.XPath.XDocument => 159
	i64 u0x730bfb248998f67a, ; 572: System.IO.Compression.ZipFile => 45
	i64 u0x732b2d67b9e5c47b, ; 573: Xamarin.Google.ErrorProne.Annotations.dll => 340
	i64 u0x734b76fdc0dc05bb, ; 574: lib_GoogleGson.dll.so => 174
	i64 u0x73a6be34e822f9d1, ; 575: lib_System.Runtime.Serialization.dll.so => 116
	i64 u0x73e4ce94e2eb6ffc, ; 576: lib_System.Memory.dll.so => 63
	i64 u0x743a1eccf080489a, ; 577: WindowsBase.dll => 165
	i64 u0x746cf89b511b4d40, ; 578: lib_Microsoft.Extensions.Diagnostics.dll.so => 187
	i64 u0x755a91767330b3d4, ; 579: lib_Microsoft.Extensions.Configuration.dll.so => 182
	i64 u0x758463c93f0d589e, ; 580: lib_Microsoft.AspNetCore.Connections.Abstractions.dll.so => 175
	i64 u0x75c326eb821b85c4, ; 581: lib_System.ComponentModel.DataAnnotations.dll.so => 14
	i64 u0x76012e7334db86e5, ; 582: lib_Xamarin.AndroidX.SavedState.dll.so => 287
	i64 u0x76ca07b878f44da0, ; 583: System.Runtime.Numerics.dll => 111
	i64 u0x7736c8a96e51a061, ; 584: lib_Xamarin.AndroidX.Annotation.Jvm.dll.so => 234
	i64 u0x778a805e625329ef, ; 585: System.Linq.Parallel => 60
	i64 u0x779290cc2b801eb7, ; 586: Xamarin.KotlinX.AtomicFU.Jvm => 373
	i64 u0x77d9074d8f33a303, ; 587: lib_System.Net.ServerSentEvents.dll.so => 224
	i64 u0x77f8a4acc2fdc449, ; 588: System.Security.Cryptography.Cng.dll => 121
	i64 u0x780bc73597a503a9, ; 589: lib-ms-Microsoft.Maui.Controls.resources.dll.so => 398
	i64 u0x782c5d8eb99ff201, ; 590: lib_Microsoft.VisualBasic.Core.dll.so => 2
	i64 u0x783606d1e53e7a1a, ; 591: th/Microsoft.Maui.Controls.resources.dll => 408
	i64 u0x784b4ff3eed363ff, ; 592: Xamarin.Firebase.Common => 309
	i64 u0x78a0275e998ae4ef, ; 593: Xamarin.Firebase.Measurement.Connector.dll => 326
	i64 u0x78a1938b89c96721, ; 594: Microsoft.AspNetCore.Http.Connections.Common => 177
	i64 u0x78a45e51311409b6, ; 595: Xamarin.AndroidX.Fragment.dll => 259
	i64 u0x78d5c74e565733ea, ; 596: Xamarin.Google.Android.DataTransport.TransportRuntime.dll => 333
	i64 u0x78ed4ab8f9d800a1, ; 597: Xamarin.AndroidX.Lifecycle.ViewModel => 273
	i64 u0x793546dbadd324b1, ; 598: Xamarin.Google.Android.DataTransport.TransportApi => 331
	i64 u0x7a25bdb29108c6e7, ; 599: Microsoft.Extensions.Http => 190
	i64 u0x7a39601d6f0bb831, ; 600: lib_Xamarin.KotlinX.AtomicFU.dll.so => 372
	i64 u0x7a7e7eddf79c5d26, ; 601: lib_Xamarin.AndroidX.Lifecycle.ViewModel.dll.so => 273
	i64 u0x7a9a57d43b0845fa, ; 602: System.AppContext => 6
	i64 u0x7ad0f4f1e5d08183, ; 603: Xamarin.AndroidX.Collection.dll => 241
	i64 u0x7adb8da2ac89b647, ; 604: fi/Microsoft.Maui.Controls.resources.dll => 388
	i64 u0x7b13d9eaa944ade8, ; 605: Xamarin.AndroidX.DynamicAnimation.dll => 255
	i64 u0x7b1644fe7249da0c, ; 606: Plugin.Firebase.Firestore => 213
	i64 u0x7b41df00b4b7f385, ; 607: Xamarin.CodeHaus.Mojo.AnimalSnifferAnnotations.dll => 302
	i64 u0x7b4927e421291c41, ; 608: Microsoft.IdentityModel.JsonWebTokens.dll => 198
	i64 u0x7bef86a4335c4870, ; 609: System.ComponentModel.TypeConverter => 17
	i64 u0x7c0820144cd34d6a, ; 610: sk/Microsoft.Maui.Controls.resources.dll => 406
	i64 u0x7c2a0bd1e0f988fc, ; 611: lib-de-Microsoft.Maui.Controls.resources.dll.so => 385
	i64 u0x7c41d387501568ba, ; 612: System.Net.WebClient.dll => 77
	i64 u0x7c482cd79bd24b13, ; 613: lib_Xamarin.AndroidX.ConstraintLayout.dll.so => 245
	i64 u0x7cb95ad2a929d044, ; 614: Xamarin.GooglePlayServices.Basement => 348
	i64 u0x7cd2ec8eaf5241cd, ; 615: System.Security.dll => 131
	i64 u0x7cf9ae50dd350622, ; 616: Xamarin.Jetbrains.Annotations.dll => 367
	i64 u0x7d49c593eeb09ac9, ; 617: Microsoft.AspNetCore.SignalR.Client.dll => 178
	i64 u0x7d649b75d580bb42, ; 618: ms/Microsoft.Maui.Controls.resources.dll => 398
	i64 u0x7d8ee2bdc8e3aad1, ; 619: System.Numerics.Vectors => 83
	i64 u0x7df5df8db8eaa6ac, ; 620: Microsoft.Extensions.Logging.Debug => 193
	i64 u0x7dfc3d6d9d8d7b70, ; 621: System.Collections => 12
	i64 u0x7e2e564fa2f76c65, ; 622: lib_System.Diagnostics.Tracing.dll.so => 34
	i64 u0x7e302e110e1e1346, ; 623: lib_System.Security.Claims.dll.so => 119
	i64 u0x7e4465b3f78ad8d0, ; 624: Xamarin.KotlinX.Serialization.Core.dll => 378
	i64 u0x7e571cad5915e6c3, ; 625: lib_Xamarin.AndroidX.Lifecycle.Process.dll.so => 268
	i64 u0x7e65340ed0da76d2, ; 626: Xamarin.Grpc.OkHttp => 362
	i64 u0x7e6b1ca712437d7d, ; 627: Xamarin.AndroidX.Emoji2.ViewsHelper => 257
	i64 u0x7e946809d6008ef2, ; 628: lib_System.ObjectModel.dll.so => 85
	i64 u0x7ea0272c1b4a9635, ; 629: lib_Xamarin.Android.Glide.dll.so => 226
	i64 u0x7eb4f0dc47488736, ; 630: lib_Xamarin.GooglePlayServices.Tasks.dll.so => 357
	i64 u0x7ecc13347c8fd849, ; 631: lib_System.ComponentModel.dll.so => 18
	i64 u0x7f00ddd9b9ca5a13, ; 632: Xamarin.AndroidX.ViewPager.dll => 298
	i64 u0x7f9351cd44b1273f, ; 633: Microsoft.Extensions.Configuration.Abstractions => 183
	i64 u0x7fbd557c99b3ce6f, ; 634: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.dll.so => 266
	i64 u0x8076a9a44a2ca331, ; 635: System.Net.Quic => 72
	i64 u0x80b7e726b0280681, ; 636: Microsoft.VisualStudio.DesignTools.MobileTapContracts => 416
	i64 u0x80da183a87731838, ; 637: System.Reflection.Metadata => 95
	i64 u0x812c069d5cdecc17, ; 638: System.dll => 164
	i64 u0x81381be520a60adb, ; 639: Xamarin.AndroidX.Interpolator.dll => 261
	i64 u0x81657cec2b31e8aa, ; 640: System.Net => 82
	i64 u0x81ab745f6c0f5ce6, ; 641: zh-Hant/Microsoft.Maui.Controls.resources => 414
	i64 u0x81db25321ea48ab3, ; 642: lib_Xamarin.Firebase.AppCheck.Interop.dll.so => 306
	i64 u0x8277f2be6b5ce05f, ; 643: Xamarin.AndroidX.AppCompat => 235
	i64 u0x828f06563b30bc50, ; 644: lib_Xamarin.AndroidX.CardView.dll.so => 240
	i64 u0x82920a8d9194a019, ; 645: Xamarin.KotlinX.AtomicFU.Jvm.dll => 373
	i64 u0x82b399cb01b531c4, ; 646: lib_System.Web.dll.so => 153
	i64 u0x82df8f5532a10c59, ; 647: lib_System.Drawing.dll.so => 36
	i64 u0x82f0b6e911d13535, ; 648: lib_System.Transactions.dll.so => 150
	i64 u0x82f6403342e12049, ; 649: uk/Microsoft.Maui.Controls.resources => 410
	i64 u0x8393689e0a5c408d, ; 650: lib_Xamarin.GooglePlayServices.Measurement.Sdk.dll.so => 354
	i64 u0x83a7afd2c49adc86, ; 651: lib_Microsoft.IdentityModel.Abstractions.dll.so => 197
	i64 u0x83c14ba66c8e2b8c, ; 652: zh-Hans/Microsoft.Maui.Controls.resources => 413
	i64 u0x846ce984efea52c7, ; 653: System.Threading.Tasks.Parallel.dll => 143
	i64 u0x846f52335a832137, ; 654: Microsoft.Extensions.Features => 189
	i64 u0x84ae73148a4557d2, ; 655: lib_System.IO.Pipes.dll.so => 56
	i64 u0x84b01102c12a9232, ; 656: System.Runtime.Serialization.Json.dll => 113
	i64 u0x850c5ba0b57ce8e7, ; 657: lib_Xamarin.AndroidX.Collection.dll.so => 241
	i64 u0x851d02edd334b044, ; 658: Xamarin.AndroidX.VectorDrawable => 295
	i64 u0x85410a0ce2b82e74, ; 659: lib_Xamarin.Grpc.Context.dll.so => 360
	i64 u0x85c919db62150978, ; 660: Xamarin.AndroidX.Transition.dll => 294
	i64 u0x85fe8aab7ca9ed11, ; 661: Xamarin.Google.Android.Recaptcha => 336
	i64 u0x8662aaeb94fef37f, ; 662: lib_System.Dynamic.Runtime.dll.so => 37
	i64 u0x86a909228dc7657b, ; 663: lib-zh-Hant-Microsoft.Maui.Controls.resources.dll.so => 414
	i64 u0x86b3e00c36b84509, ; 664: Microsoft.Extensions.Configuration.dll => 182
	i64 u0x86b62cb077ec4fd7, ; 665: System.Runtime.Serialization.Xml => 115
	i64 u0x8706ffb12bf3f53d, ; 666: Xamarin.AndroidX.Annotation.Experimental => 233
	i64 u0x872a5b14c18d328c, ; 667: System.ComponentModel.DataAnnotations => 14
	i64 u0x872fb9615bc2dff0, ; 668: Xamarin.Android.Glide.Annotations.dll => 227
	i64 u0x8794c7c19600413d, ; 669: Xamarin.Grpc.Protobuf.Lite => 363
	i64 u0x87b7bede2c8fef74, ; 670: Xamarin.Firebase.ProtoliteWellKnownTypes.dll => 328
	i64 u0x87c69b87d9283884, ; 671: lib_System.Threading.Thread.dll.so => 145
	i64 u0x87f6569b25707834, ; 672: System.IO.Compression.Brotli.dll => 43
	i64 u0x87fe2bfaae9f8412, ; 673: lib_Plugin.Firebase.Auth.dll.so => 208
	i64 u0x8842b3a5d2d3fb36, ; 674: Microsoft.Maui.Essentials => 204
	i64 u0x88926583efe7ee86, ; 675: Xamarin.AndroidX.Activity.Ktx.dll => 231
	i64 u0x88b16a1a7051ebe2, ; 676: Xamarin.Firebase.Annotations.dll => 305
	i64 u0x88ba6bc4f7762b03, ; 677: lib_System.Reflection.dll.so => 98
	i64 u0x88bda98e0cffb7a9, ; 678: lib_Xamarin.KotlinX.Coroutines.Core.Jvm.dll.so => 376
	i64 u0x8930322c7bd8f768, ; 679: netstandard => 167
	i64 u0x897a606c9e39c75f, ; 680: lib_System.ComponentModel.Primitives.dll.so => 16
	i64 u0x89911a22005b92b7, ; 681: System.IO.FileSystem.DriveInfo.dll => 48
	i64 u0x89c5188089ec2cd5, ; 682: lib_System.Runtime.InteropServices.RuntimeInformation.dll.so => 107
	i64 u0x8a19e3dc71b34b2c, ; 683: System.Reflection.TypeExtensions.dll => 97
	i64 u0x8ad229ea26432ee2, ; 684: Xamarin.AndroidX.Loader => 277
	i64 u0x8b42b55a5bb040b5, ; 685: lib_Microsoft.AspNetCore.SignalR.Protocols.Json.dll.so => 181
	i64 u0x8b4ff5d0fdd5faa1, ; 686: lib_System.Diagnostics.DiagnosticSource.dll.so => 27
	i64 u0x8b541d476eb3774c, ; 687: System.Security.Principal.Windows => 128
	i64 u0x8b8d01333a96d0b5, ; 688: System.Diagnostics.Process.dll => 29
	i64 u0x8b9278242f21e276, ; 689: Xamarin.Firebase.Encoders.JSON.dll => 318
	i64 u0x8b9ceca7acae3451, ; 690: lib-he-Microsoft.Maui.Controls.resources.dll.so => 390
	i64 u0x8c230514448fef34, ; 691: Xamarin.Firebase.ProtoliteWellKnownTypes => 328
	i64 u0x8cb8f612b633affb, ; 692: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 288
	i64 u0x8cdfdb4ce85fb925, ; 693: lib_System.Security.Principal.Windows.dll.so => 128
	i64 u0x8cdfe7b8f4caa426, ; 694: System.IO.Compression.FileSystem => 44
	i64 u0x8d0f420977c2c1c7, ; 695: Xamarin.AndroidX.CursorAdapter.dll => 250
	i64 u0x8d29ad8fa9631e25, ; 696: lib_Xamarin.GooglePlayServices.Ads.Identifier.dll.so => 345
	i64 u0x8d52f7ea2796c531, ; 697: Xamarin.AndroidX.Emoji2.dll => 256
	i64 u0x8d7b8ab4b3310ead, ; 698: System.Threading => 148
	i64 u0x8da188285aadfe8e, ; 699: System.Collections.Concurrent => 8
	i64 u0x8ed807bfe9858dfc, ; 700: Xamarin.AndroidX.Navigation.Common => 279
	i64 u0x8ee08b8194a30f48, ; 701: lib-hi-Microsoft.Maui.Controls.resources.dll.so => 391
	i64 u0x8ef7601039857a44, ; 702: lib-ro-Microsoft.Maui.Controls.resources.dll.so => 404
	i64 u0x8efbc0801a122264, ; 703: Xamarin.GooglePlayServices.Tasks.dll => 357
	i64 u0x8f32c6f611f6ffab, ; 704: pt/Microsoft.Maui.Controls.resources.dll => 403
	i64 u0x8f44b45eb046bbd1, ; 705: System.ServiceModel.Web.dll => 132
	i64 u0x8f8829d21c8985a4, ; 706: lib-pt-BR-Microsoft.Maui.Controls.resources.dll.so => 402
	i64 u0x8fbf5b0114c6dcef, ; 707: System.Globalization.dll => 42
	i64 u0x8fcc8c2a81f3d9e7, ; 708: Xamarin.KotlinX.Serialization.Core => 378
	i64 u0x8fd42635e63de49e, ; 709: Xamarin.Grpc.Context.dll => 360
	i64 u0x8feffa44c5924324, ; 710: lib_Xamarin.Protobuf.JavaLite.dll.so => 380
	i64 u0x90263f8448b8f572, ; 711: lib_System.Diagnostics.TraceSource.dll.so => 33
	i64 u0x903101b46fb73a04, ; 712: _Microsoft.Android.Resource.Designer => 419
	i64 u0x90393bd4865292f3, ; 713: lib_System.IO.Compression.dll.so => 46
	i64 u0x905e2b8e7ae91ae6, ; 714: System.Threading.Tasks.Extensions.dll => 142
	i64 u0x90634f86c5ebe2b5, ; 715: Xamarin.AndroidX.Lifecycle.ViewModel.Android => 274
	i64 u0x907b636704ad79ef, ; 716: lib_Microsoft.Maui.Controls.Xaml.dll.so => 202
	i64 u0x90ae2b5b8b652f2a, ; 717: lib_Microsoft.AspNetCore.SignalR.Client.Core.dll.so => 179
	i64 u0x90e9efbfd68593e0, ; 718: lib_Xamarin.AndroidX.Lifecycle.LiveData.dll.so => 265
	i64 u0x910c160571ecf437, ; 719: Xamarin.Firebase.Abt.dll => 303
	i64 u0x91413101e5d9f995, ; 720: Xamarin.Firebase.Auth => 307
	i64 u0x91418dc638b29e68, ; 721: lib_Xamarin.AndroidX.CustomView.dll.so => 251
	i64 u0x9157bd523cd7ed36, ; 722: lib_System.Text.Json.dll.so => 138
	i64 u0x91a74f07b30d37e2, ; 723: System.Linq.dll => 62
	i64 u0x91cb86ea3b17111d, ; 724: System.ServiceModel.Web => 132
	i64 u0x91fa41a87223399f, ; 725: ca/Microsoft.Maui.Controls.resources.dll => 382
	i64 u0x92054e486c0c7ea7, ; 726: System.IO.FileSystem.DriveInfo => 48
	i64 u0x928614058c40c4cd, ; 727: lib_System.Xml.XPath.XDocument.dll.so => 159
	i64 u0x92a698e6d582778f, ; 728: Xamarin.Firebase.Components.dll => 311
	i64 u0x92b138fffca2b01e, ; 729: lib_Xamarin.AndroidX.Arch.Core.Runtime.dll.so => 238
	i64 u0x92dfc2bfc6c6a888, ; 730: Xamarin.AndroidX.Lifecycle.LiveData => 265
	i64 u0x933da2c779423d68, ; 731: Xamarin.Android.Glide.Annotations => 227
	i64 u0x9388aad9b7ae40ce, ; 732: lib_Xamarin.AndroidX.Lifecycle.Common.dll.so => 263
	i64 u0x939ce2bba50a7f7a, ; 733: Xamarin.Firebase.Crashlytics => 313
	i64 u0x93a39f9bbc3b3ca4, ; 734: Xamarin.GooglePlayServices.CloudMessaging.dll => 349
	i64 u0x93cfa73ab28d6e35, ; 735: ms/Microsoft.Maui.Controls.resources => 398
	i64 u0x941c00d21e5c0679, ; 736: lib_Xamarin.AndroidX.Transition.dll.so => 294
	i64 u0x944077d8ca3c6580, ; 737: System.IO.Compression.dll => 46
	i64 u0x948cffedc8ed7960, ; 738: System.Xml => 163
	i64 u0x948d746a7702861f, ; 739: Microsoft.IdentityModel.Logging.dll => 199
	i64 u0x94c8990839c4bdb1, ; 740: lib_Xamarin.AndroidX.Interpolator.dll.so => 261
	i64 u0x9564283c37ed59a9, ; 741: lib_Microsoft.IdentityModel.Logging.dll.so => 199
	i64 u0x957a4cdfdcfd6d83, ; 742: Refit.HttpClientFactory.dll => 218
	i64 u0x95c96189b88c95f7, ; 743: Plugin.Firebase.Analytics => 207
	i64 u0x9603ff572969049c, ; 744: Xamarin.Firebase.Analytics.dll => 304
	i64 u0x967fc325e09bfa8c, ; 745: es/Microsoft.Maui.Controls.resources => 387
	i64 u0x9686161486d34b81, ; 746: lib_Xamarin.AndroidX.ExifInterface.dll.so => 258
	i64 u0x9732d8dbddea3d9a, ; 747: id/Microsoft.Maui.Controls.resources => 394
	i64 u0x97695ad5e6dbb63d, ; 748: Xamarin.Io.PerfMark.PerfMarkApi.dll => 365
	i64 u0x978be80e5210d31b, ; 749: Microsoft.Maui.Graphics.dll => 205
	i64 u0x979ab54025cc1c7f, ; 750: lib_Xamarin.GooglePlayServices.Base.dll.so => 347
	i64 u0x97b8c771ea3e4220, ; 751: System.ComponentModel.dll => 18
	i64 u0x97e144c9d3c6976e, ; 752: System.Collections.Concurrent.dll => 8
	i64 u0x97e55f96df4ddd72, ; 753: lib_Xamarin.Firebase.Annotations.dll.so => 305
	i64 u0x984184e3c70d4419, ; 754: GoogleGson => 174
	i64 u0x9843944103683dd3, ; 755: Xamarin.AndroidX.Core.Core.Ktx => 249
	i64 u0x98af17e841bbadcb, ; 756: Xamarin.Firebase.Storage.dll => 329
	i64 u0x98d720cc4597562c, ; 757: System.Security.Cryptography.OpenSsl => 124
	i64 u0x991d510397f92d9d, ; 758: System.Linq.Expressions => 59
	i64 u0x996ceeb8a3da3d67, ; 759: System.Threading.Overlapped.dll => 140
	i64 u0x999cb19e1a04ffd3, ; 760: CommunityToolkit.Mvvm.dll => 173
	i64 u0x99a00ca5270c6878, ; 761: Xamarin.AndroidX.Navigation.Runtime => 281
	i64 u0x99cdc6d1f2d3a72f, ; 762: ko/Microsoft.Maui.Controls.resources.dll => 397
	i64 u0x9a01b1da98b6ee10, ; 763: Xamarin.AndroidX.Lifecycle.Runtime.dll => 269
	i64 u0x9a5ccc274fd6e6ee, ; 764: Jsr305Binding.dll => 337
	i64 u0x9aca298b5a43ad5c, ; 765: Plugin.Firebase.Core.dll => 210
	i64 u0x9ae6940b11c02876, ; 766: lib_Xamarin.AndroidX.Window.dll.so => 300
	i64 u0x9b211a749105beac, ; 767: System.Transactions.Local => 149
	i64 u0x9b76bea3357ab1a1, ; 768: Xamarin.GooglePlayServices.Measurement.Api.dll => 351
	i64 u0x9b8734714671022d, ; 769: System.Threading.Tasks.Dataflow.dll => 141
	i64 u0x9bb3e2d53d840e51, ; 770: Xamarin.Protobuf.JavaLite.dll => 380
	i64 u0x9bc6aea27fbf034f, ; 771: lib_Xamarin.KotlinX.Coroutines.Core.dll.so => 375
	i64 u0x9bd8cc74558ad4c7, ; 772: Xamarin.KotlinX.AtomicFU => 372
	i64 u0x9c244ac7cda32d26, ; 773: System.Security.Cryptography.X509Certificates.dll => 126
	i64 u0x9c465f280cf43733, ; 774: lib_Xamarin.KotlinX.Coroutines.Android.dll.so => 374
	i64 u0x9c4dd9126a77a711, ; 775: Xamarin.Firebase.Installations.InterOp => 325
	i64 u0x9c8f6872beab6408, ; 776: System.Xml.XPath.XDocument.dll => 159
	i64 u0x9ce01cf91101ae23, ; 777: System.Xml.XmlDocument => 161
	i64 u0x9d128180c81d7ce6, ; 778: Xamarin.AndroidX.CustomView.PoolingContainer => 252
	i64 u0x9d5dbcf5a48583fe, ; 779: lib_Xamarin.AndroidX.Activity.dll.so => 230
	i64 u0x9d74dee1a7725f34, ; 780: Microsoft.Extensions.Configuration.Abstractions.dll => 183
	i64 u0x9dd9025fdfb738aa, ; 781: Plugin.Firebase.Crashlytics.dll => 211
	i64 u0x9de64d56c52fba95, ; 782: Xamarin.Firebase.Encoders.Proto => 319
	i64 u0x9df1696bb97cdfbb, ; 783: Xamarin.GooglePlayServices.Measurement.Sdk.dll => 354
	i64 u0x9e4534b6adaf6e84, ; 784: nl/Microsoft.Maui.Controls.resources => 400
	i64 u0x9e4b95dec42769f7, ; 785: System.Diagnostics.Debug.dll => 26
	i64 u0x9eaf1efdf6f7267e, ; 786: Xamarin.AndroidX.Navigation.Common.dll => 279
	i64 u0x9ef542cf1f78c506, ; 787: Xamarin.AndroidX.Lifecycle.LiveData.Core => 266
	i64 u0xa00832eb975f56a8, ; 788: lib_System.Net.dll.so => 82
	i64 u0xa05475503f80b7d9, ; 789: Microsoft.AspNetCore.Connections.Abstractions => 175
	i64 u0xa0ad78236b7b267f, ; 790: Xamarin.AndroidX.Window => 300
	i64 u0xa0d8259f4cc284ec, ; 791: lib_System.Security.Cryptography.dll.so => 127
	i64 u0xa0e17ca50c77a225, ; 792: lib_Xamarin.Google.Crypto.Tink.Android.dll.so => 338
	i64 u0xa0ff9b3e34d92f11, ; 793: lib_System.Resources.Writer.dll.so => 101
	i64 u0xa12fbfb4da97d9f3, ; 794: System.Threading.Timer.dll => 147
	i64 u0xa1440773ee9d341e, ; 795: Xamarin.Google.Android.Material => 334
	i64 u0xa1b9d7c27f47219f, ; 796: Xamarin.AndroidX.Navigation.UI.dll => 282
	i64 u0xa2572680829d2c7c, ; 797: System.IO.Pipelines.dll => 54
	i64 u0xa258230b8fad57ac, ; 798: lib_Xamarin.Io.PerfMark.PerfMarkApi.dll.so => 365
	i64 u0xa26597e57ee9c7f6, ; 799: System.Xml.XmlDocument.dll => 161
	i64 u0xa308401900e5bed3, ; 800: lib_mscorlib.dll.so => 166
	i64 u0xa395572e7da6c99d, ; 801: lib_System.Security.dll.so => 131
	i64 u0xa3e683f24b43af6f, ; 802: System.Dynamic.Runtime.dll => 37
	i64 u0xa4145becdee3dc4f, ; 803: Xamarin.AndroidX.VectorDrawable.Animated => 296
	i64 u0xa46aa1eaa214539b, ; 804: ko/Microsoft.Maui.Controls.resources => 397
	i64 u0xa4a372eecb9e4df0, ; 805: Microsoft.Extensions.Diagnostics => 187
	i64 u0xa4d20d2ff0563d26, ; 806: lib_CommunityToolkit.Mvvm.dll.so => 173
	i64 u0xa4edc8f2ceae241a, ; 807: System.Data.Common.dll => 22
	i64 u0xa5072f596f97bd71, ; 808: lib_Plugin.Firebase.Crashlytics.dll.so => 211
	i64 u0xa5494f40f128ce6a, ; 809: System.Runtime.Serialization.Formatters.dll => 112
	i64 u0xa54b74df83dce92b, ; 810: System.Reflection.DispatchProxy => 90
	i64 u0xa579ed010d7e5215, ; 811: Xamarin.AndroidX.DocumentFile => 253
	i64 u0xa5b7152421ed6d98, ; 812: lib_System.IO.FileSystem.Watcher.dll.so => 50
	i64 u0xa5c3844f17b822db, ; 813: lib_System.Linq.Parallel.dll.so => 60
	i64 u0xa5ce5c755bde8cb8, ; 814: lib_System.Security.Cryptography.Csp.dll.so => 122
	i64 u0xa5e599d1e0524750, ; 815: System.Numerics.Vectors.dll => 83
	i64 u0xa5f1ba49b85dd355, ; 816: System.Security.Cryptography.dll => 127
	i64 u0xa61975a5a37873ea, ; 817: lib_System.Xml.XmlSerializer.dll.so => 162
	i64 u0xa6593e21584384d2, ; 818: lib_Jsr305Binding.dll.so => 337
	i64 u0xa66cbee0130865f7, ; 819: lib_WindowsBase.dll.so => 165
	i64 u0xa67dbee13e1df9ca, ; 820: Xamarin.AndroidX.SavedState.dll => 287
	i64 u0xa684b098dd27b296, ; 821: lib_Xamarin.AndroidX.Security.SecurityCrypto.dll.so => 289
	i64 u0xa68a420042bb9b1f, ; 822: Xamarin.AndroidX.DrawerLayout.dll => 254
	i64 u0xa6d26156d1cacc7c, ; 823: Xamarin.Android.Glide.dll => 226
	i64 u0xa75386b5cb9595aa, ; 824: Xamarin.AndroidX.Lifecycle.Runtime.Android => 270
	i64 u0xa763fbb98df8d9fb, ; 825: lib_Microsoft.Win32.Primitives.dll.so => 4
	i64 u0xa78ce3745383236a, ; 826: Xamarin.AndroidX.Lifecycle.Common.Jvm => 264
	i64 u0xa7a08e0f3bd00c3e, ; 827: Xamarin.Firebase.Messaging.dll => 327
	i64 u0xa7c31b56b4dc7b33, ; 828: hu/Microsoft.Maui.Controls.resources => 393
	i64 u0xa7eab29ed44b4e7a, ; 829: Mono.Android.Export => 169
	i64 u0xa8195217cbf017b7, ; 830: Microsoft.VisualBasic.Core => 2
	i64 u0xa843f6095f0d247d, ; 831: Xamarin.GooglePlayServices.Base.dll => 347
	i64 u0xa859a95830f367ff, ; 832: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll.so => 275
	i64 u0xa8b52f21e0dbe690, ; 833: System.Runtime.Serialization.dll => 116
	i64 u0xa8c84ce526c2b4bd, ; 834: Microsoft.VisualStudio.DesignTools.XamlTapContract.dll => 418
	i64 u0xa8e6320dd07580ef, ; 835: lib_Microsoft.IdentityModel.JsonWebTokens.dll.so => 198
	i64 u0xa8ee4ed7de2efaee, ; 836: Xamarin.AndroidX.Annotation.dll => 232
	i64 u0xa91bd3f9fcc9ed32, ; 837: Xamarin.GooglePlayServices.Measurement.Sdk => 354
	i64 u0xa9302afdfc455810, ; 838: Xamarin.GoogleAndroid.Annotations.dll => 344
	i64 u0xa95590e7c57438a4, ; 839: System.Configuration => 19
	i64 u0xaa2219c8e3449ff5, ; 840: Microsoft.Extensions.Logging.Abstractions => 192
	i64 u0xaa2de94d374e55df, ; 841: Xamarin.Firebase.Common.Ktx => 310
	i64 u0xaa443ac34067eeef, ; 842: System.Private.Xml.dll => 89
	i64 u0xaa52de307ef5d1dd, ; 843: System.Net.Http => 65
	i64 u0xaa571d3c32740d65, ; 844: Plugin.Firebase.Auth => 208
	i64 u0xaa9a7b0214a5cc5c, ; 845: System.Diagnostics.StackTrace.dll => 30
	i64 u0xaaaf86367285a918, ; 846: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 186
	i64 u0xaaf84bb3f052a265, ; 847: el/Microsoft.Maui.Controls.resources => 386
	i64 u0xab9af77b5b67a0b8, ; 848: Xamarin.AndroidX.ConstraintLayout.Core => 246
	i64 u0xab9c1b2687d86b0b, ; 849: lib_System.Linq.Expressions.dll.so => 59
	i64 u0xac03339b985f4d59, ; 850: Microsoft.AspNetCore.SignalR.Client.Core.dll => 179
	i64 u0xac2af3fa195a15ce, ; 851: System.Runtime.Numerics => 111
	i64 u0xac5376a2a538dc10, ; 852: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 266
	i64 u0xac5acae88f60357e, ; 853: System.Diagnostics.Tools.dll => 32
	i64 u0xac79c7e46047ad98, ; 854: System.Security.Principal.Windows.dll => 128
	i64 u0xac98d31068e24591, ; 855: System.Xml.XDocument => 158
	i64 u0xacd46e002c3ccb97, ; 856: ro/Microsoft.Maui.Controls.resources => 404
	i64 u0xacda2fab67639416, ; 857: Xamarin.Firebase.Installations => 324
	i64 u0xacdd9e4180d56dda, ; 858: Xamarin.AndroidX.Concurrent.Futures => 244
	i64 u0xacf42eea7ef9cd12, ; 859: System.Threading.Channels => 225
	i64 u0xad7e82ed3b0f16d0, ; 860: lib_Xamarin.AndroidX.DocumentFile.dll.so => 253
	i64 u0xad89c07347f1bad6, ; 861: nl/Microsoft.Maui.Controls.resources.dll => 400
	i64 u0xadbb53caf78a79d2, ; 862: System.Web.HttpUtility => 152
	i64 u0xadc90ab061a9e6e4, ; 863: System.ComponentModel.TypeConverter.dll => 17
	i64 u0xadca1b9030b9317e, ; 864: Xamarin.AndroidX.Collection.Ktx => 243
	i64 u0xadd8eda2edf396ad, ; 865: Xamarin.Android.Glide.GifDecoder => 229
	i64 u0xadf4cf30debbeb9a, ; 866: System.Net.ServicePoint.dll => 75
	i64 u0xadf511667bef3595, ; 867: System.Net.Security => 74
	i64 u0xae0aaa94fdcfce0f, ; 868: System.ComponentModel.EventBasedAsync.dll => 15
	i64 u0xae282bcd03739de7, ; 869: Java.Interop => 168
	i64 u0xae52d4a130fb9b72, ; 870: lib_Xamarin.Firebase.Analytics.dll.so => 304
	i64 u0xae53579c90db1107, ; 871: System.ObjectModel.dll => 85
	i64 u0xaeb080014622ef84, ; 872: Xamarin.JavaX.Inject => 366
	i64 u0xaec7c0c7e2ed4575, ; 873: lib_Xamarin.KotlinX.AtomicFU.Jvm.dll.so => 373
	i64 u0xaf732d0b2193b8f5, ; 874: System.Security.Cryptography.OpenSsl.dll => 124
	i64 u0xaf871db261e04601, ; 875: lib_Xamarin.Firebase.Crashlytics.dll.so => 313
	i64 u0xafdb94dbccd9d11c, ; 876: Xamarin.AndroidX.Lifecycle.LiveData.dll => 265
	i64 u0xafe29f45095518e7, ; 877: lib_Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll.so => 276
	i64 u0xb03ae931fb25607e, ; 878: Xamarin.AndroidX.ConstraintLayout => 245
	i64 u0xb05cc42cd94c6d9d, ; 879: lib-sv-Microsoft.Maui.Controls.resources.dll.so => 407
	i64 u0xb0ac21bec8f428c5, ; 880: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.Android.dll => 272
	i64 u0xb0bb43dc52ea59f9, ; 881: System.Diagnostics.Tracing.dll => 34
	i64 u0xb1dd05401aa8ee63, ; 882: System.Security.AccessControl => 118
	i64 u0xb220631954820169, ; 883: System.Text.RegularExpressions => 139
	i64 u0xb2376e1dbf8b4ed7, ; 884: System.Security.Cryptography.Csp => 122
	i64 u0xb2a1959fe95c5402, ; 885: lib_System.Runtime.InteropServices.JavaScript.dll.so => 106
	i64 u0xb2a3f67f3bf29fce, ; 886: da/Microsoft.Maui.Controls.resources => 384
	i64 u0xb3005ac9c8a035c5, ; 887: lib_Xamarin.JavaX.Inject.dll.so => 366
	i64 u0xb3011a0a57f7ffb2, ; 888: Microsoft.VisualStudio.DesignTools.MobileTapContracts.dll => 416
	i64 u0xb363b43eeec5798e, ; 889: lib_Xamarin.Firebase.Iid.Interop.dll.so => 323
	i64 u0xb3874072ee0ecf8c, ; 890: Xamarin.AndroidX.VectorDrawable.Animated.dll => 296
	i64 u0xb3f0a0fcda8d3ebc, ; 891: Xamarin.AndroidX.CardView => 240
	i64 u0xb404462cdf3bffdb, ; 892: lib_Xamarin.Firebase.ProtoliteWellKnownTypes.dll.so => 328
	i64 u0xb46be1aa6d4fff93, ; 893: hi/Microsoft.Maui.Controls.resources => 391
	i64 u0xb477491be13109d8, ; 894: ar/Microsoft.Maui.Controls.resources => 381
	i64 u0xb4b4a8794e933f72, ; 895: lib_Plugin.Firebase.Firestore.dll.so => 213
	i64 u0xb4bd7015ecee9d86, ; 896: System.IO.Pipelines => 54
	i64 u0xb4c53d9749c5f226, ; 897: lib_System.IO.FileSystem.AccessControl.dll.so => 47
	i64 u0xb4f8071f1e28b58e, ; 898: Xamarin.Firebase.AppCheck.Interop.dll => 306
	i64 u0xb4ff710863453fda, ; 899: System.Diagnostics.FileVersionInfo.dll => 28
	i64 u0xb565e07395edb5ad, ; 900: Xamarin.Firebase.Storage.Common => 330
	i64 u0xb5c38bf497a4cfe2, ; 901: lib_System.Threading.Tasks.dll.so => 144
	i64 u0xb5c7fcdafbc67ee4, ; 902: Microsoft.Extensions.Logging.Abstractions.dll => 192
	i64 u0xb5e2ea1bb00704d6, ; 903: Xamarin.Kotlin.StdLib.Jdk7.dll => 370
	i64 u0xb5ea31d5244c6626, ; 904: System.Threading.ThreadPool.dll => 146
	i64 u0xb66575354464a3ec, ; 905: Xamarin.Kotlin.StdLib.Common => 369
	i64 u0xb685a1c17fee494b, ; 906: Xamarin.Firebase.Crashlytics.dll => 313
	i64 u0xb7212c4683a94afe, ; 907: System.Drawing.Primitives => 35
	i64 u0xb7b7753d1f319409, ; 908: sv/Microsoft.Maui.Controls.resources => 407
	i64 u0xb81a2c6e0aee50fe, ; 909: lib_System.Private.CoreLib.dll.so => 172
	i64 u0xb872c26142d22aa9, ; 910: Microsoft.Extensions.Http.dll => 190
	i64 u0xb8b0a9b3dfbc5cb7, ; 911: Xamarin.AndroidX.Window.Extensions.Core.Core => 301
	i64 u0xb8c60af47c08d4da, ; 912: System.Net.ServicePoint => 75
	i64 u0xb8e68d20aad91196, ; 913: lib_System.Xml.XPath.dll.so => 160
	i64 u0xb9185c33a1643eed, ; 914: Microsoft.CSharp.dll => 1
	i64 u0xb97187307f9dba5e, ; 915: Xamarin.KotlinX.Coroutines.Play.Services.dll => 377
	i64 u0xb9b8001adf4ed7cc, ; 916: lib_Xamarin.AndroidX.SlidingPaneLayout.dll.so => 290
	i64 u0xb9f64d3b230def68, ; 917: lib-pt-Microsoft.Maui.Controls.resources.dll.so => 403
	i64 u0xb9fc3c8a556e3691, ; 918: ja/Microsoft.Maui.Controls.resources => 396
	i64 u0xba4670aa94a2b3c6, ; 919: lib_System.Xml.XDocument.dll.so => 158
	i64 u0xba48785529705af9, ; 920: System.Collections.dll => 12
	i64 u0xba965b8c86359996, ; 921: lib_System.Windows.dll.so => 154
	i64 u0xbb286883bc35db36, ; 922: System.Transactions.dll => 150
	i64 u0xbb65706fde942ce3, ; 923: System.Net.Sockets => 76
	i64 u0xbba28979413cad9e, ; 924: lib_System.Runtime.CompilerServices.VisualC.dll.so => 103
	i64 u0xbbd180354b67271a, ; 925: System.Runtime.Serialization.Formatters => 112
	i64 u0xbc0e640e7c6bcdf8, ; 926: Xamarin.Grpc.Api => 359
	i64 u0xbc260cdba33291a3, ; 927: Xamarin.AndroidX.Arch.Core.Common.dll => 237
	i64 u0xbc727ece861e474e, ; 928: lib_Square.OkIO.JVM.dll.so => 222
	i64 u0xbcfd4e14fffb3bb4, ; 929: MvvmHelpers.dll => 219
	i64 u0xbd0e2c0d55246576, ; 930: System.Net.Http.dll => 65
	i64 u0xbd3fbd85b9e1cb29, ; 931: lib_System.Net.HttpListener.dll.so => 66
	i64 u0xbd437a2cdb333d0d, ; 932: Xamarin.AndroidX.ViewPager2 => 299
	i64 u0xbd47da6a92fd7307, ; 933: Xamarin.Firebase.Config.dll => 312
	i64 u0xbd4f572d2bd0a789, ; 934: System.IO.Compression.ZipFile.dll => 45
	i64 u0xbd5d0b88d3d647a5, ; 935: lib_Xamarin.AndroidX.Browser.dll.so => 239
	i64 u0xbd877b14d0b56392, ; 936: System.Runtime.Intrinsics.dll => 109
	i64 u0xbd960c672af2c51e, ; 937: Xamarin.GooglePlayServices.Stats => 356
	i64 u0xbe65a49036345cf4, ; 938: lib_System.Buffers.dll.so => 7
	i64 u0xbee38d4a88835966, ; 939: Xamarin.AndroidX.AppCompat.AppCompatResources => 236
	i64 u0xbef9919db45b4ca7, ; 940: System.IO.Pipes.AccessControl => 55
	i64 u0xbf0fa68611139208, ; 941: lib_Xamarin.AndroidX.Annotation.dll.so => 232
	i64 u0xbfc1e1fb3095f2b3, ; 942: lib_System.Net.Http.Json.dll.so => 64
	i64 u0xc040a4ab55817f58, ; 943: ar/Microsoft.Maui.Controls.resources.dll => 381
	i64 u0xc07cadab29efeba0, ; 944: Xamarin.AndroidX.Core.Core.Ktx.dll => 249
	i64 u0xc0d928351ab5ca77, ; 945: System.Console.dll => 20
	i64 u0xc0f5a221a9383aea, ; 946: System.Runtime.Intrinsics => 109
	i64 u0xc111030af54d7191, ; 947: System.Resources.Writer => 101
	i64 u0xc12b8b3afa48329c, ; 948: lib_System.Linq.dll.so => 62
	i64 u0xc156694d1a6d7cd6, ; 949: Plugin.Firebase.dll => 206
	i64 u0xc183ca0b74453aa9, ; 950: lib_System.Threading.Tasks.Dataflow.dll.so => 141
	i64 u0xc1ff9ae3cdb6e1e6, ; 951: Xamarin.AndroidX.Activity.dll => 230
	i64 u0xc226d517e7e30388, ; 952: lib_Xamarin.Grpc.Stub.dll.so => 364
	i64 u0xc26c064effb1dea9, ; 953: System.Buffers.dll => 7
	i64 u0xc278de356ad8a9e3, ; 954: Microsoft.IdentityModel.Logging => 199
	i64 u0xc28c50f32f81cc73, ; 955: ja/Microsoft.Maui.Controls.resources.dll => 396
	i64 u0xc2902f6cf5452577, ; 956: lib_Mono.Android.Export.dll.so => 169
	i64 u0xc2a3bca55b573141, ; 957: System.IO.FileSystem.Watcher => 50
	i64 u0xc2bcfec99f69365e, ; 958: Xamarin.AndroidX.ViewPager2.dll => 299
	i64 u0xc30b52815b58ac2c, ; 959: lib_System.Runtime.Serialization.Xml.dll.so => 115
	i64 u0xc36d7d89c652f455, ; 960: System.Threading.Overlapped => 140
	i64 u0xc396b285e59e5493, ; 961: GoogleGson.dll => 174
	i64 u0xc39ced8467203460, ; 962: lib_Refit.HttpClientFactory.dll.so => 218
	i64 u0xc3c86c1e5e12f03d, ; 963: WindowsBase => 165
	i64 u0xc421b61fd853169d, ; 964: lib_System.Net.WebSockets.Client.dll.so => 80
	i64 u0xc4628cb75a5204b4, ; 965: lib_Xamarin.GooglePlayServices.CloudMessaging.dll.so => 349
	i64 u0xc463e077917aa21d, ; 966: System.Runtime.Serialization.Json => 113
	i64 u0xc491e38137aa3bcf, ; 967: Xamarin.Firebase.Storage => 329
	i64 u0xc4d3858ed4d08512, ; 968: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 276
	i64 u0xc4f2d57c50beb816, ; 969: lib_Microsoft.Extensions.Features.dll.so => 189
	i64 u0xc50fded0ded1418c, ; 970: lib_System.ComponentModel.TypeConverter.dll.so => 17
	i64 u0xc519125d6bc8fb11, ; 971: lib_System.Net.Requests.dll.so => 73
	i64 u0xc5293b19e4dc230e, ; 972: Xamarin.AndroidX.Navigation.Fragment => 280
	i64 u0xc5325b2fcb37446f, ; 973: lib_System.Private.Xml.dll.so => 89
	i64 u0xc535cb9a21385d9b, ; 974: lib_Xamarin.Android.Glide.DiskLruCache.dll.so => 228
	i64 u0xc5a0f4b95a699af7, ; 975: lib_System.Private.Uri.dll.so => 87
	i64 u0xc5cdcd5b6277579e, ; 976: lib_System.Security.Cryptography.Algorithms.dll.so => 120
	i64 u0xc5de3dcae13c325f, ; 977: Microsoft.AspNetCore.SignalR.Client => 178
	i64 u0xc5ec286825cb0bf4, ; 978: Xamarin.AndroidX.Tracing.Tracing => 293
	i64 u0xc62af3e2d6d38289, ; 979: lib_Xamarin.Firebase.Firestore.dll.so => 320
	i64 u0xc6706bc8aa7fe265, ; 980: Xamarin.AndroidX.Annotation.Jvm => 234
	i64 u0xc7c01e7d7c93a110, ; 981: System.Text.Encoding.Extensions.dll => 135
	i64 u0xc7ce851898a4548e, ; 982: lib_System.Web.HttpUtility.dll.so => 152
	i64 u0xc809d4089d2556b2, ; 983: System.Runtime.InteropServices.JavaScript.dll => 106
	i64 u0xc858a28d9ee5a6c5, ; 984: lib_System.Collections.Specialized.dll.so => 11
	i64 u0xc8ac7c6bf1c2ec51, ; 985: System.Reflection.DispatchProxy.dll => 90
	i64 u0xc8b01aafa0eb7f80, ; 986: Xamarin.Firebase.Iid.dll => 322
	i64 u0xc9c62c8f354ac568, ; 987: lib_System.Diagnostics.TextWriterTraceListener.dll.so => 31
	i64 u0xca3a723e7342c5b6, ; 988: lib-tr-Microsoft.Maui.Controls.resources.dll.so => 409
	i64 u0xca5801070d9fccfb, ; 989: System.Text.Encoding => 136
	i64 u0xcab3493c70141c2d, ; 990: pl/Microsoft.Maui.Controls.resources => 401
	i64 u0xcacfddc9f7c6de76, ; 991: ro/Microsoft.Maui.Controls.resources.dll => 404
	i64 u0xcadbc92899a777f0, ; 992: Xamarin.AndroidX.Startup.StartupRuntime => 291
	i64 u0xcba1cb79f45292b5, ; 993: Xamarin.Android.Glide.GifDecoder.dll => 229
	i64 u0xcbb5f80c7293e696, ; 994: lib_System.Globalization.Calendars.dll.so => 40
	i64 u0xcbd4fdd9cef4a294, ; 995: lib__Microsoft.Android.Resource.Designer.dll.so => 419
	i64 u0xcc15da1e07bbd994, ; 996: Xamarin.AndroidX.SlidingPaneLayout => 290
	i64 u0xcc2876b32ef2794c, ; 997: lib_System.Text.RegularExpressions.dll.so => 139
	i64 u0xcc5c3bb714c4561e, ; 998: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 376
	i64 u0xcc76886e09b88260, ; 999: Xamarin.KotlinX.Serialization.Core.Jvm.dll => 379
	i64 u0xcc9fa2923aa1c9ef, ; 1000: System.Diagnostics.Contracts.dll => 25
	i64 u0xcce5f0b382db16b7, ; 1001: Microsoft.AspNetCore.Http.Connections.Client => 176
	i64 u0xccf25c4b634ccd3a, ; 1002: zh-Hans/Microsoft.Maui.Controls.resources.dll => 413
	i64 u0xcd10a42808629144, ; 1003: System.Net.Requests => 73
	i64 u0xcd82116fea47859e, ; 1004: Plugin.Firebase.CloudMessaging.dll => 209
	i64 u0xcdca1b920e9f53ba, ; 1005: Xamarin.AndroidX.Interpolator => 261
	i64 u0xcdd0c48b6937b21c, ; 1006: Xamarin.AndroidX.SwipeRefreshLayout => 292
	i64 u0xcde1fa22dc303670, ; 1007: Microsoft.VisualStudio.DesignTools.XamlTapContract => 418
	i64 u0xce9594d842cf35a9, ; 1008: lib_Xamarin.Firebase.Common.Ktx.dll.so => 310
	i64 u0xcf1f7a2359f1a539, ; 1009: Xamarin.JavaX.Inject.dll => 366
	i64 u0xcf23d8093f3ceadf, ; 1010: System.Diagnostics.DiagnosticSource.dll => 27
	i64 u0xcf5ff6b6b2c4c382, ; 1011: System.Net.Mail.dll => 67
	i64 u0xcf8fc898f98b0d34, ; 1012: System.Private.Xml.Linq => 88
	i64 u0xd04b5f59ed596e31, ; 1013: System.Reflection.Metadata.dll => 95
	i64 u0xd063299fcfc0c93f, ; 1014: lib_System.Runtime.Serialization.Json.dll.so => 113
	i64 u0xd07eb0f20de63ff6, ; 1015: Xamarin.Firebase.Encoders.JSON => 318
	i64 u0xd0de8a113e976700, ; 1016: System.Diagnostics.TextWriterTraceListener => 31
	i64 u0xd0fc33d5ae5d4cb8, ; 1017: System.Runtime.Extensions => 104
	i64 u0xd100f453fb70440c, ; 1018: Xamarin.Firebase.Functions => 321
	i64 u0xd1194e1d8a8de83c, ; 1019: lib_Xamarin.AndroidX.Lifecycle.Common.Jvm.dll.so => 264
	i64 u0xd12beacdfc14f696, ; 1020: System.Dynamic.Runtime => 37
	i64 u0xd14743e7ab3d6623, ; 1021: lib_Xamarin.Firebase.Config.dll.so => 312
	i64 u0xd16fd7fb9bbcd43e, ; 1022: Microsoft.Extensions.Diagnostics.Abstractions => 188
	i64 u0xd198e7ce1b6a8344, ; 1023: System.Net.Quic.dll => 72
	i64 u0xd20588bdafd1c17c, ; 1024: Xamarin.Grpc.Stub.dll => 364
	i64 u0xd3144156a3727ebe, ; 1025: Xamarin.Google.Guava.ListenableFuture => 343
	i64 u0xd333d0af9e423810, ; 1026: System.Runtime.InteropServices => 108
	i64 u0xd33a415cb4278969, ; 1027: System.Security.Cryptography.Encoding.dll => 123
	i64 u0xd3426d966bb704f5, ; 1028: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 236
	i64 u0xd3651b6fc3125825, ; 1029: System.Private.Uri.dll => 87
	i64 u0xd373685349b1fe8b, ; 1030: Microsoft.Extensions.Logging.dll => 191
	i64 u0xd3801faafafb7698, ; 1031: System.Private.DataContractSerialization.dll => 86
	i64 u0xd3e4c8d6a2d5d470, ; 1032: it/Microsoft.Maui.Controls.resources => 395
	i64 u0xd3edcc1f25459a50, ; 1033: System.Reflection.Emit => 93
	i64 u0xd4645626dffec99d, ; 1034: lib_Microsoft.Extensions.DependencyInjection.Abstractions.dll.so => 186
	i64 u0xd4fa0abb79079ea9, ; 1035: System.Security.Principal.dll => 129
	i64 u0xd52f53c4b3d62e11, ; 1036: Microsoft.AspNetCore.Connections.Abstractions.dll => 175
	i64 u0xd5507e11a2b2839f, ; 1037: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 276
	i64 u0xd553306bfc9d438f, ; 1038: Plugin.Firebase.Functions => 214
	i64 u0xd5d04bef8478ea19, ; 1039: Xamarin.AndroidX.Tracing.Tracing.dll => 293
	i64 u0xd5d562f8d2ec1c98, ; 1040: Xamarin.GooglePlayServices.Measurement.Impl.dll => 353
	i64 u0xd60815f26a12e140, ; 1041: Microsoft.Extensions.Logging.Debug.dll => 193
	i64 u0xd65786d27a4ad960, ; 1042: lib_Microsoft.Maui.Controls.HotReload.Forms.dll.so => 415
	i64 u0xd6694f8359737e4e, ; 1043: Xamarin.AndroidX.SavedState => 287
	i64 u0xd6949e129339eae5, ; 1044: lib_Xamarin.AndroidX.Core.Core.Ktx.dll.so => 249
	i64 u0xd6d21782156bc35b, ; 1045: Xamarin.AndroidX.SwipeRefreshLayout.dll => 292
	i64 u0xd6de019f6af72435, ; 1046: Xamarin.AndroidX.ConstraintLayout.Core.dll => 246
	i64 u0xd6ed09ee80649430, ; 1047: lib_Xamarin.Grpc.Core.dll.so => 361
	i64 u0xd70956d1e6deefb9, ; 1048: Jsr305Binding => 337
	i64 u0xd72329819cbbbc44, ; 1049: lib_Microsoft.Extensions.Configuration.Abstractions.dll.so => 183
	i64 u0xd72c760af136e863, ; 1050: System.Xml.XmlSerializer.dll => 162
	i64 u0xd74883ff25771958, ; 1051: Xamarin.Io.PerfMark.PerfMarkApi => 365
	i64 u0xd74f2028e57f6faf, ; 1052: Plugin.Firebase.Storage => 216
	i64 u0xd753f071e44c2a03, ; 1053: lib_System.Security.SecureString.dll.so => 130
	i64 u0xd7b3764ada9d341d, ; 1054: lib_Microsoft.Extensions.Logging.Abstractions.dll.so => 192
	i64 u0xd7dfd89d34e8dd1d, ; 1055: Square.OkIO => 221
	i64 u0xd7e8ce535a3e02a1, ; 1056: lib_Plugin.Firebase.Analytics.dll.so => 207
	i64 u0xd7f0088bc5ad71f2, ; 1057: Xamarin.AndroidX.VersionedParcelable => 297
	i64 u0xd7f086420957bfa6, ; 1058: lib_Plugin.Firebase.CloudMessaging.dll.so => 209
	i64 u0xd896bbf4bbb77722, ; 1059: Plugin.Firebase.Crashlytics => 211
	i64 u0xd8fb25e28ae30a12, ; 1060: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 284
	i64 u0xda1dfa4c534a9251, ; 1061: Microsoft.Extensions.DependencyInjection => 185
	i64 u0xdab94026b007e005, ; 1062: Xamarin.GooglePlayServices.Auth.Api.Phone.dll => 346
	i64 u0xdad05a11827959a3, ; 1063: System.Collections.NonGeneric.dll => 10
	i64 u0xdaefdfe71aa53cf9, ; 1064: System.IO.FileSystem.Primitives => 49
	i64 u0xdaff1e02a729f3a2, ; 1065: Xamarin.Grpc.Android => 358
	i64 u0xdb5383ab5865c007, ; 1066: lib-vi-Microsoft.Maui.Controls.resources.dll.so => 411
	i64 u0xdb58816721c02a59, ; 1067: lib_System.Reflection.Emit.ILGeneration.dll.so => 91
	i64 u0xdbeda89f832aa805, ; 1068: vi/Microsoft.Maui.Controls.resources.dll => 411
	i64 u0xdbf2a779fbc3ac31, ; 1069: System.Transactions.Local.dll => 149
	i64 u0xdbf9607a441b4505, ; 1070: System.Linq => 62
	i64 u0xdbfc90157a0de9b0, ; 1071: lib_System.Text.Encoding.dll.so => 136
	i64 u0xdc75032002d1a212, ; 1072: lib_System.Transactions.Local.dll.so => 149
	i64 u0xdca8be7403f92d4f, ; 1073: lib_System.Linq.Queryable.dll.so => 61
	i64 u0xdce2c53525640bf3, ; 1074: Microsoft.Extensions.Logging => 191
	i64 u0xdd2b722d78ef5f43, ; 1075: System.Runtime.dll => 117
	i64 u0xdd67031857c72f96, ; 1076: lib_System.Text.Encodings.Web.dll.so => 137
	i64 u0xdd92e229ad292030, ; 1077: System.Numerics.dll => 84
	i64 u0xddaac4821f63b25f, ; 1078: lib_Xamarin.GooglePlayServices.Measurement.Sdk.Api.dll.so => 355
	i64 u0xdddcdd701e911af1, ; 1079: lib_Xamarin.AndroidX.Legacy.Support.Core.Utils.dll.so => 262
	i64 u0xdde30e6b77aa6f6c, ; 1080: lib-zh-Hans-Microsoft.Maui.Controls.resources.dll.so => 413
	i64 u0xde110ae80fa7c2e2, ; 1081: System.Xml.XDocument.dll => 158
	i64 u0xde4726fcdf63a198, ; 1082: Xamarin.AndroidX.Transition => 294
	i64 u0xde572c2b2fb32f93, ; 1083: lib_System.Threading.Tasks.Extensions.dll.so => 142
	i64 u0xde8769ebda7d8647, ; 1084: hr/Microsoft.Maui.Controls.resources.dll => 392
	i64 u0xdea791ecb8d4fb73, ; 1085: Xamarin.Google.Android.Play.Integrity.dll => 335
	i64 u0xdee075f3477ef6be, ; 1086: Xamarin.AndroidX.ExifInterface.dll => 258
	i64 u0xdee9c5f72a60d9b7, ; 1087: Xamarin.GooglePlayServices.Measurement.Sdk.Api.dll => 355
	i64 u0xdf4b773de8fb1540, ; 1088: System.Net.dll => 82
	i64 u0xdf5cc1fa03af29c1, ; 1089: Xamarin.GooglePlayServices.Measurement.dll => 350
	i64 u0xdf9c7682560a9629, ; 1090: System.Net.ServerSentEvents => 224
	i64 u0xdfa254ebb4346068, ; 1091: System.Net.Ping => 70
	i64 u0xe0142572c095a480, ; 1092: Xamarin.AndroidX.AppCompat.dll => 235
	i64 u0xe021eaa401792a05, ; 1093: System.Text.Encoding.dll => 136
	i64 u0xe02f89350ec78051, ; 1094: Xamarin.AndroidX.CoordinatorLayout.dll => 247
	i64 u0xe02ff568f8e5f275, ; 1095: Microsoft.AspNetCore.Http.Connections.Client.dll => 176
	i64 u0xe03ac59b394077a7, ; 1096: lib_Xamarin.Firebase.Messaging.dll.so => 327
	i64 u0xe0496b9d65ef5474, ; 1097: Xamarin.Android.Glide.DiskLruCache.dll => 228
	i64 u0xe10b760bb1462e7a, ; 1098: lib_System.Security.Cryptography.Primitives.dll.so => 125
	i64 u0xe14ff8e84737288a, ; 1099: Xamarin.Firebase.Installations.InterOp.dll => 325
	i64 u0xe1566bbdb759c5af, ; 1100: Microsoft.Maui.Controls.HotReload.Forms.dll => 415
	i64 u0xe192a588d4410686, ; 1101: lib_System.IO.Pipelines.dll.so => 54
	i64 u0xe1a08bd3fa539e0d, ; 1102: System.Runtime.Loader => 110
	i64 u0xe1a77eb8831f7741, ; 1103: System.Security.SecureString.dll => 130
	i64 u0xe1b52f9f816c70ef, ; 1104: System.Private.Xml.Linq.dll => 88
	i64 u0xe1de400f85bb1ea2, ; 1105: Xamarin.GooglePlayServices.Measurement.Impl => 353
	i64 u0xe1e199c8ab02e356, ; 1106: System.Data.DataSetExtensions.dll => 23
	i64 u0xe1ecfdb7fff86067, ; 1107: System.Net.Security.dll => 74
	i64 u0xe2252a80fe853de4, ; 1108: lib_System.Security.Principal.dll.so => 129
	i64 u0xe22fa4c9c645db62, ; 1109: System.Diagnostics.TextWriterTraceListener.dll => 31
	i64 u0xe2420585aeceb728, ; 1110: System.Net.Requests.dll => 73
	i64 u0xe247baa54eab2a37, ; 1111: Xamarin.Firebase.Common.Ktx.dll => 310
	i64 u0xe26692647e6bcb62, ; 1112: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 271
	i64 u0xe2799d9c4661dbbc, ; 1113: Xamarin.GoogleAndroid.Annotations => 344
	i64 u0xe27a4eed1bd77064, ; 1114: Xamarin.Firebase.Abt => 303
	i64 u0xe29b73bc11392966, ; 1115: lib-id-Microsoft.Maui.Controls.resources.dll.so => 394
	i64 u0xe2a295b75d36df94, ; 1116: Xamarin.Firebase.Auth.dll => 307
	i64 u0xe2ad448dee50fbdf, ; 1117: System.Xml.Serialization => 157
	i64 u0xe2d920f978f5d85c, ; 1118: System.Data.DataSetExtensions => 23
	i64 u0xe2e426c7714fa0bc, ; 1119: Microsoft.Win32.Primitives.dll => 4
	i64 u0xe332bacb3eb4a806, ; 1120: Mono.Android.Export.dll => 169
	i64 u0xe3811d68d4fe8463, ; 1121: pt-BR/Microsoft.Maui.Controls.resources.dll => 402
	i64 u0xe392dad93475f892, ; 1122: Xamarin.Firebase.Config => 312
	i64 u0xe3b7cbae5ad66c75, ; 1123: lib_System.Security.Cryptography.Encoding.dll.so => 123
	i64 u0xe3f96d37a010f8d7, ; 1124: lib_Xamarin.Firebase.Dynamic.Links.dll.so => 316
	i64 u0xe494f7ced4ecd10a, ; 1125: hu/Microsoft.Maui.Controls.resources.dll => 393
	i64 u0xe4a9b1e40d1e8917, ; 1126: lib-fi-Microsoft.Maui.Controls.resources.dll.so => 388
	i64 u0xe4f74a0b5bf9703f, ; 1127: System.Runtime.Serialization.Primitives => 114
	i64 u0xe5434e8a119ceb69, ; 1128: lib_Mono.Android.dll.so => 171
	i64 u0xe55703b9ce5c038a, ; 1129: System.Diagnostics.Tools => 32
	i64 u0xe57013c8afc270b5, ; 1130: Microsoft.VisualBasic => 3
	i64 u0xe62913cc36bc07ec, ; 1131: System.Xml.dll => 163
	i64 u0xe7bea09c4900a191, ; 1132: Xamarin.AndroidX.VectorDrawable.dll => 295
	i64 u0xe7e03cc18dcdeb49, ; 1133: lib_System.Diagnostics.StackTrace.dll.so => 30
	i64 u0xe7e147ff99a7a380, ; 1134: lib_System.Configuration.dll.so => 19
	i64 u0xe836920fb94f1cc4, ; 1135: Square.OkHttp3 => 220
	i64 u0xe8397cf3948e7cb7, ; 1136: lib_Microsoft.Extensions.Options.ConfigurationExtensions.dll.so => 195
	i64 u0xe83ddbccfc6aff3f, ; 1137: Xamarin.Kotlin.StdLib.Jdk7 => 370
	i64 u0xe86b0df4ba9e5db8, ; 1138: lib_Xamarin.AndroidX.Lifecycle.Runtime.Android.dll.so => 270
	i64 u0xe896622fe0902957, ; 1139: System.Reflection.Emit.dll => 93
	i64 u0xe89a2a9ef110899b, ; 1140: System.Drawing.dll => 36
	i64 u0xe8c5f8c100b5934b, ; 1141: Microsoft.Win32.Registry => 5
	i64 u0xe8e7b675503ab70a, ; 1142: lib_Xamarin.Firebase.Functions.dll.so => 321
	i64 u0xe8efe6c2171f7cd2, ; 1143: Xamarin.Google.Guava.dll => 341
	i64 u0xe93ca41931f1f2d0, ; 1144: Xamarin.Grpc.Api.dll => 359
	i64 u0xe957c3976986ab72, ; 1145: lib_Xamarin.AndroidX.Window.Extensions.Core.Core.dll.so => 301
	i64 u0xe968ab252d3dda82, ; 1146: lib_Xamarin.Google.Android.DataTransport.TransportRuntime.dll.so => 333
	i64 u0xe98163eb702ae5c5, ; 1147: Xamarin.AndroidX.Arch.Core.Runtime => 238
	i64 u0xe994f23ba4c143e5, ; 1148: Xamarin.KotlinX.Coroutines.Android => 374
	i64 u0xe9b9c8c0458fd92a, ; 1149: System.Windows => 154
	i64 u0xe9d166d87a7f2bdb, ; 1150: lib_Xamarin.AndroidX.Startup.StartupRuntime.dll.so => 291
	i64 u0xea5a4efc2ad81d1b, ; 1151: Xamarin.Google.ErrorProne.Annotations => 340
	i64 u0xeb05d8ab87158d10, ; 1152: Plugin.Firebase => 206
	i64 u0xeb2313fe9d65b785, ; 1153: Xamarin.AndroidX.ConstraintLayout.dll => 245
	i64 u0xeb6e275e78cb8d42, ; 1154: Xamarin.AndroidX.LocalBroadcastManager.dll => 278
	i64 u0xeb710a05c0467d46, ; 1155: Xamarin.GooglePlayServices.CloudMessaging => 349
	i64 u0xeb9973cda26e858f, ; 1156: Xamarin.Firebase.Common.dll => 309
	i64 u0xebbcc2bc51cb4a56, ; 1157: Plugin.Firebase.Firestore.dll => 213
	i64 u0xed19c616b3fcb7eb, ; 1158: Xamarin.AndroidX.VersionedParcelable.dll => 297
	i64 u0xed60c6fa891c051a, ; 1159: lib_Microsoft.VisualStudio.DesignTools.TapContract.dll.so => 417
	i64 u0xed71cc7167e5bfad, ; 1160: lib_Plugin.Firebase.DynamicLinks.dll.so => 212
	i64 u0xedc4817167106c23, ; 1161: System.Net.Sockets.dll => 76
	i64 u0xedc632067fb20ff3, ; 1162: System.Memory.dll => 63
	i64 u0xedc8e4ca71a02a8b, ; 1163: Xamarin.AndroidX.Navigation.Runtime.dll => 281
	i64 u0xee81f5b3f1c4f83b, ; 1164: System.Threading.ThreadPool => 146
	i64 u0xeeb7ebb80150501b, ; 1165: lib_Xamarin.AndroidX.Collection.Jvm.dll.so => 242
	i64 u0xeec0ecc44f1bf592, ; 1166: lib_Xamarin.Firebase.Storage.dll.so => 329
	i64 u0xeefc635595ef57f0, ; 1167: System.Security.Cryptography.Cng => 121
	i64 u0xef03b1b5a04e9709, ; 1168: System.Text.Encoding.CodePages.dll => 134
	i64 u0xef34290f50652a6c, ; 1169: HRMApp => 0
	i64 u0xef432781d5667f61, ; 1170: Xamarin.AndroidX.Print => 283
	i64 u0xef602c523fe2e87a, ; 1171: lib_Xamarin.Google.Guava.ListenableFuture.dll.so => 343
	i64 u0xef72742e1bcca27a, ; 1172: Microsoft.Maui.Essentials.dll => 204
	i64 u0xefd1e0c4e5c9b371, ; 1173: System.Resources.ResourceManager.dll => 100
	i64 u0xefe8f8d5ed3c72ea, ; 1174: System.Formats.Tar.dll => 39
	i64 u0xefec0b7fdc57ec42, ; 1175: Xamarin.AndroidX.Activity => 230
	i64 u0xf00c29406ea45e19, ; 1176: es/Microsoft.Maui.Controls.resources.dll => 387
	i64 u0xf038bf84c0c27e83, ; 1177: lib_Microsoft.AspNetCore.SignalR.Client.dll.so => 178
	i64 u0xf09e47b6ae914f6e, ; 1178: System.Net.NameResolution => 68
	i64 u0xf0ac2b489fed2e35, ; 1179: lib_System.Diagnostics.Debug.dll.so => 26
	i64 u0xf0bb49dadd3a1fe1, ; 1180: lib_System.Net.ServicePoint.dll.so => 75
	i64 u0xf0de2537ee19c6ca, ; 1181: lib_System.Net.WebHeaderCollection.dll.so => 78
	i64 u0xf1138779fa181c68, ; 1182: lib_Xamarin.AndroidX.Lifecycle.Runtime.dll.so => 269
	i64 u0xf11b621fc87b983f, ; 1183: Microsoft.Maui.Controls.Xaml.dll => 202
	i64 u0xf161f4f3c3b7e62c, ; 1184: System.Data => 24
	i64 u0xf16eb650d5a464bc, ; 1185: System.ValueTuple => 151
	i64 u0xf1c4b4005493d871, ; 1186: System.Formats.Asn1.dll => 38
	i64 u0xf2039b1a33e63e8e, ; 1187: Xamarin.Google.Android.DataTransport.TransportApi.dll => 331
	i64 u0xf238bd79489d3a96, ; 1188: lib-nl-Microsoft.Maui.Controls.resources.dll.so => 400
	i64 u0xf2a69492c6bd46b0, ; 1189: lib_Xamarin.Kotlin.StdLib.Jdk7.dll.so => 370
	i64 u0xf2feea356ba760af, ; 1190: Xamarin.AndroidX.Arch.Core.Runtime.dll => 238
	i64 u0xf300e085f8acd238, ; 1191: lib_System.ServiceProcess.dll.so => 133
	i64 u0xf34e52b26e7e059d, ; 1192: System.Runtime.CompilerServices.VisualC.dll => 103
	i64 u0xf37221fda4ef8830, ; 1193: lib_Xamarin.Google.Android.Material.dll.so => 334
	i64 u0xf3ad9b8fb3eefd12, ; 1194: lib_System.IO.UnmanagedMemoryStream.dll.so => 57
	i64 u0xf3ddfe05336abf29, ; 1195: System => 164
	i64 u0xf3f82de7e22a8032, ; 1196: Xamarin.GooglePlayServices.Ads.Identifier.dll => 345
	i64 u0xf408654b2a135055, ; 1197: System.Reflection.Emit.ILGeneration.dll => 91
	i64 u0xf4103170a1de5bd0, ; 1198: System.Linq.Queryable.dll => 61
	i64 u0xf42d20c23173d77c, ; 1199: lib_System.ServiceModel.Web.dll.so => 132
	i64 u0xf483be3bba89b4ff, ; 1200: lib_Xamarin.Grpc.Api.dll.so => 359
	i64 u0xf4c1dd70a5496a17, ; 1201: System.IO.Compression => 46
	i64 u0xf4ecf4b9afc64781, ; 1202: System.ServiceProcess.dll => 133
	i64 u0xf4eeeaa566e9b970, ; 1203: lib_Xamarin.AndroidX.CustomView.PoolingContainer.dll.so => 252
	i64 u0xf518f63ead11fcd1, ; 1204: System.Threading.Tasks => 144
	i64 u0xf5fc7602fe27b333, ; 1205: System.Net.WebHeaderCollection => 78
	i64 u0xf6077741019d7428, ; 1206: Xamarin.AndroidX.CoordinatorLayout => 247
	i64 u0xf61ade9836ad4692, ; 1207: Microsoft.IdentityModel.Tokens.dll => 200
	i64 u0xf6742cbf457c450b, ; 1208: Xamarin.AndroidX.Lifecycle.Runtime.Android.dll => 270
	i64 u0xf6c0e7d55a7a4e4f, ; 1209: Microsoft.IdentityModel.JsonWebTokens => 198
	i64 u0xf6f893f692f8cb43, ; 1210: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 195
	i64 u0xf70c0a7bf8ccf5af, ; 1211: System.Web => 153
	i64 u0xf73b506af603bfe1, ; 1212: lib_Square.OkIO.dll.so => 221
	i64 u0xf77b20923f07c667, ; 1213: de/Microsoft.Maui.Controls.resources.dll => 385
	i64 u0xf7e2cac4c45067b3, ; 1214: lib_System.Numerics.Vectors.dll.so => 83
	i64 u0xf7e74930e0e3d214, ; 1215: zh-HK/Microsoft.Maui.Controls.resources.dll => 412
	i64 u0xf84773b5c81e3cef, ; 1216: lib-uk-Microsoft.Maui.Controls.resources.dll.so => 410
	i64 u0xf895bc445b304688, ; 1217: lib_Plugin.Firebase.Storage.dll.so => 216
	i64 u0xf8aac5ea82de1348, ; 1218: System.Linq.Queryable => 61
	i64 u0xf8b77539b362d3ba, ; 1219: lib_System.Reflection.Primitives.dll.so => 96
	i64 u0xf8dacc6dd9573437, ; 1220: Square.OkIO.dll => 221
	i64 u0xf8e045dc345b2ea3, ; 1221: lib_Xamarin.AndroidX.RecyclerView.dll.so => 285
	i64 u0xf915dc29808193a1, ; 1222: System.Web.HttpUtility.dll => 152
	i64 u0xf96c777a2a0686f4, ; 1223: hi/Microsoft.Maui.Controls.resources.dll => 391
	i64 u0xf9be54c8bcf8ff3b, ; 1224: System.Security.AccessControl.dll => 118
	i64 u0xf9eec5bb3a6aedc6, ; 1225: Microsoft.Extensions.Options => 194
	i64 u0xfa0e82300e67f913, ; 1226: lib_System.AppContext.dll.so => 6
	i64 u0xfa2fdb27e8a2c8e8, ; 1227: System.ComponentModel.EventBasedAsync => 15
	i64 u0xfa3f278f288b0e84, ; 1228: lib_System.Net.Security.dll.so => 74
	i64 u0xfa5ed7226d978949, ; 1229: lib-ar-Microsoft.Maui.Controls.resources.dll.so => 381
	i64 u0xfa645d91e9fc4cba, ; 1230: System.Threading.Thread => 145
	i64 u0xfad4d2c770e827f9, ; 1231: lib_System.IO.IsolatedStorage.dll.so => 52
	i64 u0xfb06dd2338e6f7c4, ; 1232: System.Net.Ping.dll => 70
	i64 u0xfb087abe5365e3b7, ; 1233: lib_System.Data.DataSetExtensions.dll.so => 23
	i64 u0xfb4159a996444a12, ; 1234: Xamarin.Firebase.Dynamic.Links.dll => 316
	i64 u0xfb846e949baff5ea, ; 1235: System.Xml.Serialization.dll => 157
	i64 u0xfbad3e4ce4b98145, ; 1236: System.Security.Cryptography.X509Certificates => 126
	i64 u0xfbf0a31c9fc34bc4, ; 1237: lib_System.Net.Http.dll.so => 65
	i64 u0xfc61ddcf78dd1f54, ; 1238: Xamarin.AndroidX.LocalBroadcastManager => 278
	i64 u0xfc6b7527cc280b3f, ; 1239: lib_System.Runtime.Serialization.Formatters.dll.so => 112
	i64 u0xfc719aec26adf9d9, ; 1240: Xamarin.AndroidX.Navigation.Fragment.dll => 280
	i64 u0xfc82690c2fe2735c, ; 1241: Xamarin.AndroidX.Lifecycle.Process.dll => 268
	i64 u0xfc93fc307d279893, ; 1242: System.IO.Pipes.AccessControl.dll => 55
	i64 u0xfcd302092ada6328, ; 1243: System.IO.MemoryMappedFiles.dll => 53
	i64 u0xfd22f00870e40ae0, ; 1244: lib_Xamarin.AndroidX.DrawerLayout.dll.so => 254
	i64 u0xfd3ce7bc9232d417, ; 1245: Xamarin.Firebase.Firestore.dll => 320
	i64 u0xfd49b3c1a76e2748, ; 1246: System.Runtime.InteropServices.RuntimeInformation => 107
	i64 u0xfd536c702f64dc47, ; 1247: System.Text.Encoding.Extensions => 135
	i64 u0xfd583f7657b6a1cb, ; 1248: Xamarin.AndroidX.Fragment => 259
	i64 u0xfd8dd91a2c26bd5d, ; 1249: Xamarin.AndroidX.Lifecycle.Runtime => 269
	i64 u0xfda36abccf05cf5c, ; 1250: System.Net.WebSockets.Client => 80
	i64 u0xfddbe9695626a7f5, ; 1251: Xamarin.AndroidX.Lifecycle.Common => 263
	i64 u0xfeae9952cf03b8cb, ; 1252: tr/Microsoft.Maui.Controls.resources => 409
	i64 u0xfeafb1b0115f77d9, ; 1253: lib_HRMApp.dll.so => 0
	i64 u0xfebe1950717515f9, ; 1254: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 267
	i64 u0xff270a55858bac8d, ; 1255: System.Security.Principal => 129
	i64 u0xff4093883e1e70fd, ; 1256: lib_Plugin.Firebase.Functions.dll.so => 214
	i64 u0xff9b54613e0d2cc8, ; 1257: System.Net.Http.Json => 64
	i64 u0xffb5607c2db1b7e8, ; 1258: Xamarin.Kotlin.StdLib.Jdk8 => 371
	i64 u0xffdb7a971be4ec73 ; 1259: System.ValueTuple.dll => 151
], align 16

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [1260 x i32] [
	i32 42, i32 375, i32 292, i32 217, i32 13, i32 180, i32 281, i32 195,
	i32 105, i32 170, i32 48, i32 235, i32 7, i32 86, i32 405, i32 383,
	i32 411, i32 197, i32 255, i32 360, i32 369, i32 71, i32 357, i32 285,
	i32 339, i32 12, i32 203, i32 102, i32 412, i32 155, i32 19, i32 260,
	i32 242, i32 160, i32 257, i32 215, i32 295, i32 166, i32 330, i32 405,
	i32 10, i32 193, i32 296, i32 96, i32 252, i32 254, i32 339, i32 13,
	i32 194, i32 10, i32 348, i32 127, i32 311, i32 95, i32 216, i32 225,
	i32 39, i32 406, i32 327, i32 379, i32 181, i32 298, i32 402, i32 171,
	i32 229, i32 5, i32 204, i32 67, i32 289, i32 130, i32 288, i32 330,
	i32 256, i32 68, i32 324, i32 243, i32 66, i32 212, i32 57, i32 251,
	i32 52, i32 43, i32 125, i32 67, i32 81, i32 271, i32 417, i32 157,
	i32 92, i32 99, i32 285, i32 140, i32 322, i32 0, i32 150, i32 239,
	i32 389, i32 161, i32 168, i32 390, i32 305, i32 302, i32 186, i32 81,
	i32 336, i32 417, i32 243, i32 179, i32 4, i32 5, i32 51, i32 101,
	i32 364, i32 350, i32 56, i32 352, i32 120, i32 98, i32 167, i32 118,
	i32 375, i32 21, i32 393, i32 137, i32 97, i32 379, i32 77, i32 399,
	i32 218, i32 283, i32 291, i32 119, i32 352, i32 8, i32 164, i32 408,
	i32 70, i32 228, i32 272, i32 286, i32 344, i32 187, i32 170, i32 317,
	i32 144, i32 40, i32 302, i32 289, i32 47, i32 30, i32 342, i32 316,
	i32 282, i32 397, i32 143, i32 321, i32 194, i32 162, i32 28, i32 84,
	i32 293, i32 339, i32 77, i32 43, i32 29, i32 42, i32 103, i32 220,
	i32 117, i32 363, i32 233, i32 45, i32 91, i32 408, i32 56, i32 147,
	i32 416, i32 348, i32 145, i32 100, i32 49, i32 210, i32 20, i32 248,
	i32 114, i32 314, i32 226, i32 219, i32 389, i32 338, i32 335, i32 210,
	i32 368, i32 196, i32 320, i32 94, i32 58, i32 223, i32 394, i32 392,
	i32 81, i32 338, i32 168, i32 26, i32 325, i32 358, i32 371, i32 71,
	i32 322, i32 284, i32 217, i32 188, i32 258, i32 415, i32 410, i32 69,
	i32 33, i32 206, i32 388, i32 14, i32 139, i32 223, i32 38, i32 414,
	i32 244, i32 332, i32 401, i32 134, i32 92, i32 88, i32 148, i32 342,
	i32 407, i32 24, i32 314, i32 215, i32 138, i32 57, i32 51, i32 386,
	i32 317, i32 189, i32 29, i32 156, i32 34, i32 163, i32 314, i32 190,
	i32 356, i32 326, i32 332, i32 259, i32 197, i32 52, i32 419, i32 300,
	i32 90, i32 315, i32 318, i32 240, i32 35, i32 389, i32 156, i32 9,
	i32 387, i32 76, i32 361, i32 55, i32 203, i32 383, i32 317, i32 201,
	i32 13, i32 307, i32 299, i32 182, i32 237, i32 109, i32 363, i32 275,
	i32 32, i32 311, i32 104, i32 84, i32 92, i32 53, i32 96, i32 367,
	i32 58, i32 9, i32 102, i32 251, i32 68, i32 298, i32 382, i32 125,
	i32 286, i32 116, i32 135, i32 200, i32 209, i32 126, i32 106, i32 368,
	i32 131, i32 362, i32 308, i32 239, i32 343, i32 146, i32 155, i32 260,
	i32 248, i32 255, i32 326, i32 286, i32 97, i32 24, i32 290, i32 142,
	i32 283, i32 279, i32 3, i32 166, i32 236, i32 207, i32 100, i32 160,
	i32 99, i32 25, i32 93, i32 167, i32 171, i32 231, i32 3, i32 401,
	i32 257, i32 351, i32 1, i32 114, i32 368, i32 260, i32 268, i32 223,
	i32 33, i32 6, i32 350, i32 405, i32 306, i32 155, i32 377, i32 403,
	i32 53, i32 262, i32 181, i32 346, i32 85, i32 309, i32 297, i32 282,
	i32 44, i32 267, i32 104, i32 47, i32 138, i32 222, i32 64, i32 355,
	i32 319, i32 277, i32 69, i32 80, i32 59, i32 89, i32 153, i32 237,
	i32 133, i32 110, i32 395, i32 277, i32 284, i32 170, i32 180, i32 304,
	i32 134, i32 319, i32 225, i32 40, i32 382, i32 345, i32 184, i32 358,
	i32 200, i32 201, i32 356, i32 60, i32 173, i32 184, i32 274, i32 308,
	i32 79, i32 25, i32 36, i32 336, i32 99, i32 271, i32 71, i32 22,
	i32 248, i32 205, i32 406, i32 212, i32 121, i32 69, i32 107, i32 412,
	i32 278, i32 119, i32 117, i32 263, i32 219, i32 264, i32 11, i32 2,
	i32 124, i32 115, i32 333, i32 141, i32 335, i32 41, i32 87, i32 342,
	i32 353, i32 232, i32 172, i32 27, i32 147, i32 184, i32 396, i32 185,
	i32 340, i32 231, i32 1, i32 214, i32 233, i32 362, i32 44, i32 247,
	i32 148, i32 262, i32 18, i32 341, i32 215, i32 177, i32 86, i32 384,
	i32 347, i32 41, i32 267, i32 241, i32 371, i32 272, i32 176, i32 94,
	i32 191, i32 28, i32 41, i32 78, i32 256, i32 244, i32 143, i32 108,
	i32 242, i32 323, i32 11, i32 105, i32 303, i32 137, i32 16, i32 122,
	i32 66, i32 156, i32 22, i32 331, i32 386, i32 378, i32 102, i32 185,
	i32 376, i32 323, i32 63, i32 58, i32 202, i32 385, i32 110, i32 172,
	i32 308, i32 341, i32 418, i32 374, i32 9, i32 334, i32 120, i32 98,
	i32 315, i32 105, i32 224, i32 377, i32 275, i32 217, i32 201, i32 111,
	i32 234, i32 49, i32 20, i32 315, i32 274, i32 250, i32 177, i32 72,
	i32 246, i32 332, i32 154, i32 39, i32 384, i32 35, i32 372, i32 38,
	i32 390, i32 301, i32 108, i32 346, i32 351, i32 399, i32 21, i32 352,
	i32 220, i32 367, i32 273, i32 222, i32 205, i32 15, i32 196, i32 79,
	i32 79, i32 250, i32 196, i32 253, i32 280, i32 288, i32 151, i32 21,
	i32 324, i32 203, i32 383, i32 50, i32 51, i32 361, i32 369, i32 409,
	i32 399, i32 94, i32 227, i32 188, i32 380, i32 395, i32 16, i32 208,
	i32 180, i32 123, i32 392, i32 159, i32 45, i32 340, i32 174, i32 116,
	i32 63, i32 165, i32 187, i32 182, i32 175, i32 14, i32 287, i32 111,
	i32 234, i32 60, i32 373, i32 224, i32 121, i32 398, i32 2, i32 408,
	i32 309, i32 326, i32 177, i32 259, i32 333, i32 273, i32 331, i32 190,
	i32 372, i32 273, i32 6, i32 241, i32 388, i32 255, i32 213, i32 302,
	i32 198, i32 17, i32 406, i32 385, i32 77, i32 245, i32 348, i32 131,
	i32 367, i32 178, i32 398, i32 83, i32 193, i32 12, i32 34, i32 119,
	i32 378, i32 268, i32 362, i32 257, i32 85, i32 226, i32 357, i32 18,
	i32 298, i32 183, i32 266, i32 72, i32 416, i32 95, i32 164, i32 261,
	i32 82, i32 414, i32 306, i32 235, i32 240, i32 373, i32 153, i32 36,
	i32 150, i32 410, i32 354, i32 197, i32 413, i32 143, i32 189, i32 56,
	i32 113, i32 241, i32 295, i32 360, i32 294, i32 336, i32 37, i32 414,
	i32 182, i32 115, i32 233, i32 14, i32 227, i32 363, i32 328, i32 145,
	i32 43, i32 208, i32 204, i32 231, i32 305, i32 98, i32 376, i32 167,
	i32 16, i32 48, i32 107, i32 97, i32 277, i32 181, i32 27, i32 128,
	i32 29, i32 318, i32 390, i32 328, i32 288, i32 128, i32 44, i32 250,
	i32 345, i32 256, i32 148, i32 8, i32 279, i32 391, i32 404, i32 357,
	i32 403, i32 132, i32 402, i32 42, i32 378, i32 360, i32 380, i32 33,
	i32 419, i32 46, i32 142, i32 274, i32 202, i32 179, i32 265, i32 303,
	i32 307, i32 251, i32 138, i32 62, i32 132, i32 382, i32 48, i32 159,
	i32 311, i32 238, i32 265, i32 227, i32 263, i32 313, i32 349, i32 398,
	i32 294, i32 46, i32 163, i32 199, i32 261, i32 199, i32 218, i32 207,
	i32 304, i32 387, i32 258, i32 394, i32 365, i32 205, i32 347, i32 18,
	i32 8, i32 305, i32 174, i32 249, i32 329, i32 124, i32 59, i32 140,
	i32 173, i32 281, i32 397, i32 269, i32 337, i32 210, i32 300, i32 149,
	i32 351, i32 141, i32 380, i32 375, i32 372, i32 126, i32 374, i32 325,
	i32 159, i32 161, i32 252, i32 230, i32 183, i32 211, i32 319, i32 354,
	i32 400, i32 26, i32 279, i32 266, i32 82, i32 175, i32 300, i32 127,
	i32 338, i32 101, i32 147, i32 334, i32 282, i32 54, i32 365, i32 161,
	i32 166, i32 131, i32 37, i32 296, i32 397, i32 187, i32 173, i32 22,
	i32 211, i32 112, i32 90, i32 253, i32 50, i32 60, i32 122, i32 83,
	i32 127, i32 162, i32 337, i32 165, i32 287, i32 289, i32 254, i32 226,
	i32 270, i32 4, i32 264, i32 327, i32 393, i32 169, i32 2, i32 347,
	i32 275, i32 116, i32 418, i32 198, i32 232, i32 354, i32 344, i32 19,
	i32 192, i32 310, i32 89, i32 65, i32 208, i32 30, i32 186, i32 386,
	i32 246, i32 59, i32 179, i32 111, i32 266, i32 32, i32 128, i32 158,
	i32 404, i32 324, i32 244, i32 225, i32 253, i32 400, i32 152, i32 17,
	i32 243, i32 229, i32 75, i32 74, i32 15, i32 168, i32 304, i32 85,
	i32 366, i32 373, i32 124, i32 313, i32 265, i32 276, i32 245, i32 407,
	i32 272, i32 34, i32 118, i32 139, i32 122, i32 106, i32 384, i32 366,
	i32 416, i32 323, i32 296, i32 240, i32 328, i32 391, i32 381, i32 213,
	i32 54, i32 47, i32 306, i32 28, i32 330, i32 144, i32 192, i32 370,
	i32 146, i32 369, i32 313, i32 35, i32 407, i32 172, i32 190, i32 301,
	i32 75, i32 160, i32 1, i32 377, i32 290, i32 403, i32 396, i32 158,
	i32 12, i32 154, i32 150, i32 76, i32 103, i32 112, i32 359, i32 237,
	i32 222, i32 219, i32 65, i32 66, i32 299, i32 312, i32 45, i32 239,
	i32 109, i32 356, i32 7, i32 236, i32 55, i32 232, i32 64, i32 381,
	i32 249, i32 20, i32 109, i32 101, i32 62, i32 206, i32 141, i32 230,
	i32 364, i32 7, i32 199, i32 396, i32 169, i32 50, i32 299, i32 115,
	i32 140, i32 174, i32 218, i32 165, i32 80, i32 349, i32 113, i32 329,
	i32 276, i32 189, i32 17, i32 73, i32 280, i32 89, i32 228, i32 87,
	i32 120, i32 178, i32 293, i32 320, i32 234, i32 135, i32 152, i32 106,
	i32 11, i32 90, i32 322, i32 31, i32 409, i32 136, i32 401, i32 404,
	i32 291, i32 229, i32 40, i32 419, i32 290, i32 139, i32 376, i32 379,
	i32 25, i32 176, i32 413, i32 73, i32 209, i32 261, i32 292, i32 418,
	i32 310, i32 366, i32 27, i32 67, i32 88, i32 95, i32 113, i32 318,
	i32 31, i32 104, i32 321, i32 264, i32 37, i32 312, i32 188, i32 72,
	i32 364, i32 343, i32 108, i32 123, i32 236, i32 87, i32 191, i32 86,
	i32 395, i32 93, i32 186, i32 129, i32 175, i32 276, i32 214, i32 293,
	i32 353, i32 193, i32 415, i32 287, i32 249, i32 292, i32 246, i32 361,
	i32 337, i32 183, i32 162, i32 365, i32 216, i32 130, i32 192, i32 221,
	i32 207, i32 297, i32 209, i32 211, i32 284, i32 185, i32 346, i32 10,
	i32 49, i32 358, i32 411, i32 91, i32 411, i32 149, i32 62, i32 136,
	i32 149, i32 61, i32 191, i32 117, i32 137, i32 84, i32 355, i32 262,
	i32 413, i32 158, i32 294, i32 142, i32 392, i32 335, i32 258, i32 355,
	i32 82, i32 350, i32 224, i32 70, i32 235, i32 136, i32 247, i32 176,
	i32 327, i32 228, i32 125, i32 325, i32 415, i32 54, i32 110, i32 130,
	i32 88, i32 353, i32 23, i32 74, i32 129, i32 31, i32 73, i32 310,
	i32 271, i32 344, i32 303, i32 394, i32 307, i32 157, i32 23, i32 4,
	i32 169, i32 402, i32 312, i32 123, i32 316, i32 393, i32 388, i32 114,
	i32 171, i32 32, i32 3, i32 163, i32 295, i32 30, i32 19, i32 220,
	i32 195, i32 370, i32 270, i32 93, i32 36, i32 5, i32 321, i32 341,
	i32 359, i32 301, i32 333, i32 238, i32 374, i32 154, i32 291, i32 340,
	i32 206, i32 245, i32 278, i32 349, i32 309, i32 213, i32 297, i32 417,
	i32 212, i32 76, i32 63, i32 281, i32 146, i32 242, i32 329, i32 121,
	i32 134, i32 0, i32 283, i32 343, i32 204, i32 100, i32 39, i32 230,
	i32 387, i32 178, i32 68, i32 26, i32 75, i32 78, i32 269, i32 202,
	i32 24, i32 151, i32 38, i32 331, i32 400, i32 370, i32 238, i32 133,
	i32 103, i32 334, i32 57, i32 164, i32 345, i32 91, i32 61, i32 132,
	i32 359, i32 46, i32 133, i32 252, i32 144, i32 78, i32 247, i32 200,
	i32 270, i32 198, i32 195, i32 153, i32 221, i32 385, i32 83, i32 412,
	i32 410, i32 216, i32 61, i32 96, i32 221, i32 285, i32 152, i32 391,
	i32 118, i32 194, i32 6, i32 15, i32 74, i32 381, i32 145, i32 52,
	i32 70, i32 23, i32 316, i32 157, i32 126, i32 65, i32 278, i32 112,
	i32 280, i32 268, i32 55, i32 53, i32 254, i32 320, i32 107, i32 135,
	i32 259, i32 269, i32 80, i32 263, i32 409, i32 0, i32 267, i32 129,
	i32 214, i32 64, i32 371, i32 151
], align 16

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 u0x0000000000000000, ; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

; Functions

; Function attributes: memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 16

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!".NET for Android remotes/origin/release/9.0.1xx @ e7876a4f92d894b40c191a24c2b74f06d4bf4573"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
