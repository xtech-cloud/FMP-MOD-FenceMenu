

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.FenceMenu.LIB.Proto;
using XTC.FMP.MOD.FenceMenu.LIB.MVCS;
using UnityEngine.Video;

namespace XTC.FMP.MOD.FenceMenu.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        public class UiReference
        {
            public RawImage background;
            public RawImage cellTemplate;
            public VideoPlayer videoPlayer;
            public RectTransform content;
            public Button btnCloseContent;
            public Transform decalS;
        }

        private UiReference uiReference_ = new UiReference();

        public MyInstance(string _uid, string _style, MyConfig _config, MyCatalog _catalog, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _catalog, _logger, _settings, _entry, _mono, _rootAttachments)
        {

        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        /// <remarks>
        /// 可用于加载主题目录的数据
        /// </remarks>
        public void HandleCreated()
        {
            uiReference_.background = rootUI.transform.Find("bg").GetComponent<RawImage>();
            uiReference_.background.gameObject.SetActive(style_.background.visible);
            uiReference_.decalS = rootUI.transform.Find("DecalS");
            uiReference_.cellTemplate = rootUI.transform.Find("container/Viewport/Content/cell").GetComponent<RawImage>();
            uiReference_.cellTemplate.gameObject.SetActive(false);
            uiReference_.videoPlayer = rootUI.transform.Find("VideoPlayer").GetComponent<VideoPlayer>();
            uiReference_.content = rootUI.transform.Find("Content").GetComponent<RectTransform>();
            uiReference_.content.gameObject.SetActive(false);
            uiReference_.btnCloseContent = rootUI.transform.Find("Content/btnClose").GetComponent<Button>();

            //Content
            alignByAncor(uiReference_.content.Find("[slot]"), style_.content.slotAnchor);
            loadTextureFromTheme(style_.content.bg, (_texture) =>
            {
                uiReference_.content.GetComponent<RawImage>().texture = _texture;
            }, () => { });

            //Content Close Button
            alignByAncor(uiReference_.btnCloseContent.transform, style_.content.btnClose.anchor);
            loadTextureFromTheme(style_.content.btnClose.image, (_texture) =>
            {
                uiReference_.btnCloseContent.GetComponent<RawImage>().texture = _texture;
                uiReference_.btnCloseContent.onClick.AddListener(() =>
                {
                    CloseContent();
                });
            }, () => { });


            // Image Background
            if (style_.background.image.active && !string.IsNullOrEmpty(style_.background.image.source))
            {
                loadTextureFromTheme(style_.background.image.source, (_texture) =>
                {
                    uiReference_.background.texture = _texture;
                }, () => { });
            }

            // Video Background
            if (style_.background.video.active && !string.IsNullOrEmpty(style_.background.video.source))
            {
                RenderTexture renderTexture = new RenderTexture((int)uiReference_.background.rectTransform.rect.width, (int)uiReference_.background.rectTransform.rect.height, 32, RenderTextureFormat.ARGB32, 0);
                uiReference_.background.texture = renderTexture;
                uiReference_.videoPlayer.targetTexture = renderTexture;

                string path = settings_["path.themes"].AsString();
                path = System.IO.Path.Combine(path, MyEntryBase.ModuleName);
                string filefullpath = System.IO.Path.Combine(path, style_.background.video.source);
                uiReference_.videoPlayer.url = filefullpath;
            }

            //Decal
            foreach (var decal in style_.decalS)
            {
                GameObject go = new GameObject("decal");
                go.transform.SetParent(uiReference_.decalS);
                var image = go.AddComponent<RawImage>();
                alignByAncor(go.transform, decal.anchor);
                loadTextureFromTheme(decal.image, (_texture) =>
                {
                    image.texture = _texture;
                }, () => { });
            }

            foreach (var cell in style_.cellS)
            {
                GameObject clone = GameObject.Instantiate(uiReference_.cellTemplate.gameObject, uiReference_.cellTemplate.transform.parent);
                var rect = clone.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(cell.size, rect.sizeDelta.y);
                loadTextureFromTheme(cell.image, (_texture) =>
                {
                    _texture.wrapMode = TextureWrapMode.Clamp;
                    clone.GetComponent<RawImage>().texture = _texture;
                    clone.SetActive(true);
                }, () => { });

                var entry = clone.transform.Find("entry");
                entry.gameObject.SetActive(false);
                if (null == cell.entry)
                    continue;

                alignByAncor(entry.transform, cell.entry.anchor);
                loadTextureFromTheme(cell.entry.image, (_texture) =>
                {
                    entry.GetComponent<RawImage>().texture = _texture;
                    entry.gameObject.SetActive(true);
                }, () => { });
                entry.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Dictionary<string, object> variableS = new Dictionary<string, object>();
                    publishSubjects(cell.subjectS, variableS);
                });
            }
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        /// <remarks>
        /// 可用于加载内容目录的数据
        /// </remarks>
        public void HandleOpened(string _source, string _uri)
        {
            uiReference_.content.gameObject.SetActive(false);
            rootUI.gameObject.SetActive(true);
            rootWorld.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
            rootWorld.gameObject.SetActive(false);
        }

        public void OpenContent()
        {
            uiReference_.content.gameObject.SetActive(true);
        }

        public void CloseContent()
        {
            uiReference_.content.gameObject.SetActive(false);
        }
    }
}
