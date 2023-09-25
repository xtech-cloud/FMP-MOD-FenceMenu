
using System;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.FenceMenu.LIB.Bridge;
using XTC.FMP.MOD.FenceMenu.LIB.MVCS;
using XTC.FMP.LIB.MVCS;
using System.Collections.Generic;

namespace XTC.FMP.MOD.FenceMenu.LIB.Unity
{
    /// <summary>
    /// 虚拟视图，用于处理消息订阅
    /// </summary>
    public class DummyView : DummyViewBase
    {
        public DummyView(string _uid) : base(_uid)
        {
            addSubscriber(MySubject.OpenContent, handleOpenContent);
            addSubscriber(MySubject.CloseContent, handleCloseContent);
        }

        protected override void setup()
        {
            base.setup();
        }

        private void handleOpenContent(Model.Status? _status, object _data)
        {
            MyInstance instance = null;
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                string uid = (string)data["uid"];
                runtime.instances.TryGetValue(uid, out instance);
            }
            catch (Exception e)
            {
                getLogger().Exception(e);
            }

            if(null == instance)
            {
                getLogger().Error("instance not found");
                return;
            }

            instance.OpenContent();
        }

        private void handleCloseContent(Model.Status? _status, object _data)
        {
            MyInstance instance = null;
            try
            {
                Dictionary<string, object> data = _data as Dictionary<string, object>;
                string uid = (string)data["uid"];
                runtime.instances.TryGetValue(uid, out instance);
            }
            catch (Exception e)
            {
                getLogger().Exception(e);
            }

            if(null == instance)
            {
                getLogger().Error("instance not found");
                return;
            }
            instance.CloseContent();
        }
    }
}

