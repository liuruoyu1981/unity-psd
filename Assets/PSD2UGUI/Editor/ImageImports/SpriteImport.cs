﻿using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;



namespace PSDUIImporter
{
    public class SpriteImport : IImageImport
    {
        public void DrawImage(Image image, GameObject parent)
        {
            switch (image.imageType)
            {
                case ImageType.Image:
                    DrawNormalImage(image, parent);
                    break;
                case ImageType.Texture:
                    DrawRawImage(image, parent);
                    break;
                case ImageType.SliceImage:
                    DrawSliceImage(image, parent);
                    break;
                default:
                    break;
            }
        }

        private void DrawRawImage(Image image, GameObject parent)
        {
            UnityEngine.UI.RawImage pic = PSDImportUtility.InstantiateItem<UnityEngine.UI.RawImage>(PSDImporterConst.PREFAB_PATH_RawIMAGE, image.name, parent);
            string assetPath =PSDImportUtility. GetPicturePath(image);
            Texture texture = UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture)) as Texture;
            pic.texture = texture;

            if (texture == null)
            {
                Debug.Log("loading asset at path: " + assetPath + "\nname:" + image.name);
                if (image.arguments.Length > 0)
                {
                    Debug.Log(image.arguments[0]);
                    Color color;
                    if (ColorUtility.TryParseHtmlString(image.arguments[0], out color))
                    {
                        pic.color = color;
                    }
                }
            }

            PSDImportUtility.SetRectTransform(image, pic.GetComponent<RectTransform>());

        }

        private void DrawNormalImage(Image image, GameObject parent)
        {
            UnityEngine.UI.Image pic = PSDImportUtility.InstantiateItem<UnityEngine.UI.Image>(PSDImporterConst.PREFAB_PATH_IMAGE, image.name, parent);

            string assetPath =PSDImportUtility. GetPicturePath(image);

            Sprite sprite = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Sprite)) as Sprite;

            if (sprite == null)
            {
                Debug.Log("loading asset at path: " + assetPath + "\nname:" + image.name);
                if (image.arguments.Length > 0)
                {
                    Debug.Log(image.arguments[0]);
                    Color color;
                    if (ColorUtility.TryParseHtmlString(image.arguments[0], out color))
                    {
                        pic.color = color;
                    }
                }
            }

            pic.sprite = sprite;

            pic.type = UnityEngine.UI.Image.Type.Simple;

            PSDImportUtility.SetRectTransform(image, pic.GetComponent<RectTransform>());
        }

        private void DrawSliceImage(Image image, GameObject parent)
        {
            UnityEngine.UI.Image pic = PSDImportUtility.InstantiateItem<UnityEngine.UI.Image>(PSDImporterConst.PREFAB_PATH_IMAGE, image.name, parent);

            string assetPath = PSDImportUtility.GetPicturePath(image);

            Sprite sprite = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Sprite)) as Sprite;

            pic.sprite = sprite;

            pic.type = UnityEngine.UI.Image.Type.Sliced;

            PSDImportUtility.SetRectTransform(image, pic.GetComponent<RectTransform>());

        }

    }
}
