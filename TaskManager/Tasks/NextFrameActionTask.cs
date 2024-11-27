// Copyright (c) 2024 Coda
// 
// This file is part of CodaGame, licensed under the MIT License.
// See the LICENSE file in the project root for license information.

using System;
using UnityGameFramework.Base;

namespace UnityGameFramework.Tasks
{
    /// <summary>
    /// A task that you can run a delegate at next frame.
    /// </summary>
    public class NextFrameActionTask : _AFrameDelayTask
    {
        private readonly Action _m_delegate;
        
        
        public NextFrameActionTask(string _name, Action _delegate, ETaskRunType _runType = ETaskRunType.Update) 
            : base(_name, 1, _runType)
        {
            _m_delegate = _delegate;
        }
        public NextFrameActionTask(Action _delegate, ETaskRunType _runType = ETaskRunType.Update)
            : this($"NextFrameActionTask_{Serialize.NextNextFrameActionTask()}", _delegate, _runType)
        {
        }
        

        protected override void OnDeal()
        {
            _m_delegate?.Invoke();
        }
        protected override void OnRun()
        {
        }
        protected override void OnStop()
        {
        }
    }
}