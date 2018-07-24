using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageProcess {

    public static float MAD(Texture2D standard, Texture2D custom, int m, int n, int i=0, int j=0)
    {
        float result = 0;
        for (int s = 1; s <= m; s++)
        {
            for (int t = 1; t <= n; t++)
            {
                float customGray = custom.GetPixel(i + s - 1, j + t - 1).grayscale;
                float standardGray = standard.GetPixel(s, t).grayscale;
                result += Mathf.Abs(customGray - standardGray);
            }
        }
        return result/(m*n);
    }

    public static byte[] getImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);

        byte[] imgByte = new byte[files.Length];

        files.Read(imgByte, 0, imgByte.Length);

        files.Close();

        return imgByte;
    }
}
