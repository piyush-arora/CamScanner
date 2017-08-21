using System;
using System.Runtime.InteropServices;

namespace OpenAR
{
    public class OpenARDllManager 
    {
        // NOTE THIS WILL BE UPDATED CONTAINING ALL INFORMATION OF IMAGE AND ITS ASSOCIATED OPERATIONS
        public struct Image
        {
            public int rows;
            public int cols;
            public int type;
            public IntPtr data;

        };


#if UNITY_EDITOR_WIN
        [DllImport("OpenAR", EntryPoint = "destroy_windows")]
        public static unsafe extern void destroy_windows();
#endif

        // NOTE WE MIGHT NOT NEED THIS FUNCTION
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        [DllImport("OpenAR", EntryPoint = "process_image")]
        public static unsafe extern void process_image(Image input_image, ref Image output_image);
#endif

        // NOTE THIS FUNCTION NEED TO BE UPDATED
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        [DllImport("OpenAR", EntryPoint = "display_webcam")]
        public static unsafe extern void display_webcam(int rows, int cols, System.IntPtr input_data, byte* output_data);

#elif (UNITY_ANDROID)
        [DllImport("libnative-lib", EntryPoint = "display_webcam")]
        public static unsafe extern void display_webcam(int rows, int cols, System.IntPtr input_data, byte* output_data);
#endif







    }
}
