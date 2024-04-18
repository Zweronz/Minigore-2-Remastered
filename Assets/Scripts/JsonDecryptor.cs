using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class JsonDecryptor : MonoBehaviour
{
    void Start()
    {
        foreach (string json in Directory.GetFiles(Application.streamingAssetsPath))
        {
            InStream stream = new InStream(json);
            int fileLength = (int)new FileInfo(json).Length;

            stream.SetDecryptionSeed((int)stream.ReadU32());
            stream.SetDecryption(true);

            byte[] data = new byte[fileLength - 4];
            for (int i = 0; i < fileLength - 4; i++)
            {
                data[i] = (byte)stream.ReadI8(); // Using ReadInternal to continue using decryption
            }

            // Convert zero bytes to spaces (as per the original C++ logic)
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 0)
                    data[i] = (byte)' ';
            }

            string jsonString = System.Text.Encoding.UTF8.GetString(data);
            File.WriteAllText(json.Replace(".json", "_converted.json"), jsonString);
        }
    }
}

public class InStream
{
    private bool decryptionEnabled;
    private int decryptionSeed;
    private int currentIndex;
    private int capacity;
    public int Capacity => capacity;
    private List<byte> buffer;
    
    public InStream(int size, byte[] initialData)
    {
        decryptionEnabled = false;
        decryptionSeed = 0;
        currentIndex = 0;
        capacity = size;
        buffer = new List<byte>(capacity);
        buffer.AddRange(initialData);
    }

    public InStream(string filePath)
    {
        decryptionEnabled = false;
        decryptionSeed = 0;
        currentIndex = 0;
        
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found.", filePath);
        
        var data = File.ReadAllBytes(filePath);
        capacity = data.Length;
        buffer = new List<byte>(data);
    }

    ~InStream()
    {
        buffer.Clear();
    }

    public void SetDecryption(bool enabled)
    {
        decryptionEnabled = enabled;
    }

    public void SetDecryptionSeed(int seed)
    {
        decryptionSeed = seed & 0x7FFF;
    }

    public uint ComputeCRC(uint initialValue, uint length)
    {
        // CRC computation would need an actual implementation or a library
        throw new NotImplementedException("CRC computation is not implemented.");
    }

    public bool ReadBool()
    {
        return ReadInternal() != 0;
    }

    private byte ReadInternal()
    {
        if (currentIndex >= capacity)
            throw new EndOfStreamException("Reached the end of the stream.");

        byte result = buffer[currentIndex++];
        if (decryptionEnabled)
        {
            int seed = (22695477 * decryptionSeed + 1);
            decryptionSeed = seed;
            result ^= (byte)((seed >> 16) ^ 0x1A);
        }

        return result;
    }

    public sbyte ReadI8()
    {
        return (sbyte)ReadInternal();
    }

    public short ReadI16()
    {
        short result = ReadInternal();
        result |= (short)(ReadInternal() << 8);
        return result;
    }

    public ushort ReadU16()
    {
        ushort result = ReadInternal();
        result |= (ushort)(ReadInternal() << 8);
        return result;
    }

    public int ReadI32()
    {
        int result = ReadInternal();
        for (int i = 1; i < 4; i++)
        {
            result |= (ReadInternal() << (8 * i));
        }
        return result;
    }

    public uint ReadU32()
    {
        uint result = ReadInternal();
        for (int i = 1; i < 4; i++)
        {
            result |= (uint)(ReadInternal() << (8 * i));
        }
        return result;
    }

    public long ReadU64()
    {
        long result = ReadInternal();
        for (int i = 1; i < 8; i++)
        {
            result |= (long)(ReadInternal() << (8 * i));
        }
        return result;
    }

    public float ReadFloat()
    {
        return BitConverter.ToSingle(BitConverter.GetBytes(ReadU32()), 0);
    }
}