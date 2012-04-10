using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace ViverMais.Model
{
    public static class Compressor
    {
        /// <summary>
        /// Method for compressing the ViewState data
        /// </summary>
        /// <param name="data">ViewState data to compress</param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            //create a new MemoryStream for holding and
            //returning the compressed ViewState
            MemoryStream output = new MemoryStream();
            //create a new GZipStream object for compressing
            //the ViewState
            GZipStream gzip = new GZipStream(output, CompressionMode.Compress, true);
            //write the compressed bytes to the underlying stream
            gzip.Write(data, 0, data.Length);
            //close the object
            gzip.Close();
            //convert the MemoryStream to an array and return
            //it to the calling method
            return output.ToArray();
        }

        /// <summary>
        /// Method for decompressing the ViewState data
        /// </summary>
        /// <param name="data">Compressed ViewState to decompress</param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            //create a MemoryStream for holding the incoming data
            MemoryStream input = new MemoryStream();
            //write the incoming bytes to the MemoryStream
            input.Write(data, 0, data.Length);
            //set our position to the start of the Stream
            input.Position = 0;
            //create an instance of the GZipStream to decompress
            //the incoming byte array (the compressed ViewState)
            GZipStream gzip = new GZipStream(input, CompressionMode.Decompress, true);
            //create a new MemoryStream for holding
            //the output
            MemoryStream output = new MemoryStream();
            //create a byte array
            byte[] buff = new byte[64];
            int read = -1;
            //read the decompressed ViewState into
            //our byte array, set that value to our
            //read variable (int data type)
            read = gzip.Read(buff, 0, buff.Length);
            //make sure we have something to read
            while (read > 0)
            {
                //write the decompressed bytes to our
                //out going MemoryStream
                output.Write(buff, 0, read);
                //get the rest of the buffer
                read = gzip.Read(buff, 0, buff.Length);
            }
            gzip.Close();
            //return our out going MemoryStream
            //in an array
            return output.ToArray();
        }
    }
}
