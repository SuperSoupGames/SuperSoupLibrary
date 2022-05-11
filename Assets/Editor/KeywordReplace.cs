/// <summary>
/// #SCRIPTNAME#
/// License: GNU AGPLv3
/// Copyright (C) #CREATIONYEAR# #DEVELOPER#
/// 
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as published
/// by the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
/// 
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
/// 
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <https://www.gnu.org/licenses/>.
///  
/// Contact: support@supersoupgames.com
/// </summary>

using UnityEngine;
using UnityEditor;


public class KeywordReplace : UnityEditor.AssetModificationProcessor
{
    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        if (index < 0)
            return;
        
        string file = path.Substring(index);
        if (file != ".cs" && file != ".js" && file != ".boo")
            return;
        
        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;
        if (!System.IO.File.Exists(path))
            return;

        string fileContent = System.IO.File.ReadAllText(path);
        fileContent = fileContent.Replace("#CREATIONDATE#", System.DateTime.Today.ToString("dd/MM/yy") + "");
        fileContent = fileContent.Replace("#CREATIONYEAR#", System.DateTime.Today.ToString("yyyy") + "");
        fileContent = fileContent.Replace("#PROJECTNAME#", PlayerSettings.productName);
        fileContent = fileContent.Replace("#DEVELOPER#", PlayerSettings.companyName);
        System.IO.File.WriteAllText(path, fileContent);
        AssetDatabase.Refresh();

    }
}

