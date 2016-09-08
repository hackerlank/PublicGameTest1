using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadUtility {

	public static T loadData<T>(string filePath){
		if(File.Exists(filePath)){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(filePath, FileMode.Open);
			T data = (T)bf.Deserialize(file);
			file.Close();
			return data;
		}
		return default(T);
	}
	
	public static void saveData<T>(T data,string filePath){
		if(!Directory.Exists(Path.GetDirectoryName(filePath)))
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(filePath, FileMode.Create);
		bf.Serialize(file, data);
		file.Close();
	}

	public static T loadDataFromResources<T>(string file){
		TextAsset asset = Resources.Load(file) as TextAsset;
		if(asset != null){
			BinaryFormatter bf = new BinaryFormatter();
			MemoryStream stream = new MemoryStream(asset.bytes);
			T data = (T)bf.Deserialize(stream);
			stream.Close();
			return data;
		}
		return default(T);
	}

	public static void saveDataToResources<T>(T data,string filePath){
		filePath = Application.dataPath+"/Resources/"+filePath;
		if(!Directory.Exists(Path.GetDirectoryName(filePath)))
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(filePath, FileMode.Create);
		bf.Serialize(file, data);
		file.Close();
	}



}
