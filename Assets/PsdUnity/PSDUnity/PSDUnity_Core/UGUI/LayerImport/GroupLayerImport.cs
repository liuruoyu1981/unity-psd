﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PSDUnity.UGUI
{
    internal class GroupLayerImport : LayerImport
    {
        public override GameObject CreateTemplate()
        {
            return new GameObject("Group", typeof(RectTransform));
        }
        public GroupLayerImport(PSDImportCtrl ctrl) : base(ctrl)
        {
        }

        public override UGUINode DrawLayer(GroupNode layer, UGUINode parent)
        {
            UGUINode node = CreateRootNode(layer.displayName, layer.rect, parent);

            if (layer.children != null)
            {
                foreach (GroupNode item in layer.children)
                {
                   var childNode = ctrl.DrawLayer(item, node);
                    SetLayoutItem(childNode, item.rect);
                }
            }
            if (layer.images != null)
            {
                foreach (ImgNode item in layer.images)
                {
                    var childNode = ctrl.DrawImage(item, node);
                    SetLayoutItem(childNode, item.rect);
                }
            }

            InitLayoutGroup(layer, node);
            return node;
        }
        private void SetLayoutItem(UGUINode childNode,Rect rect)
        {
            var layout = childNode.transform.gameObject.AddComponent<LayoutElement>();
            layout.preferredWidth = rect.width;
            layout.preferredHeight = rect.height;
            childNode.anchoType = AnchoType.Left | AnchoType.Up;
        }
        
        /// <summary>
        /// 初始化组
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="node"></param>
        private void InitLayoutGroup(GroupNode layer, UGUINode node)
        {
            HorizontalOrVerticalLayoutGroup group = null;

            switch (layer.direction)
            {
                case Direction.Horizontal:
                    group = node.InitComponent<UnityEngine.UI.HorizontalLayoutGroup>();
                    break;
                case Direction.Vertical:
                default:
                    group = node.InitComponent<UnityEngine.UI.VerticalLayoutGroup>();
                    break;
            }
            if(group)
            {
                (group as UnityEngine.UI.VerticalLayoutGroup).spacing = layer.spacing;
                group.childAlignment = TextAnchor.UpperLeft;
            }
        }
        public override void AfterGenerate(UGUINode node)
        {
            base.AfterGenerate(node);
            ContentSizeFitter content = node.InitComponent<ContentSizeFitter>();
            content.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            content.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
    }
}