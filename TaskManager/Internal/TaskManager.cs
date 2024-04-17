﻿
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameFramework.Tasks
{
    /// <summary>
    /// Internal class, which you can ignore.
    /// </summary>
    /// <remarks>
    /// <see cref="_ATask"/> will use singleton of this.
    /// </remarks>
    internal sealed class TaskManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton, use by <see cref="_ATask"/>.
        /// </summary>
        [NotNull] internal static TaskManager instance
        {
            get
            {
                if (_g_instance == null)
                {
                    GameObject go = new GameObject("TaskManager");
                    DontDestroyOnLoad(go);
                    _g_instance = go.AddComponent<TaskManager>();
                }

                // ReSharper disable once AssignNullToNotNullAttribute
                return _g_instance;
            }
        }
        // Just a singleton.
        private static TaskManager _g_instance;
        
        
        // Task will run in Update and LateUpdate, use Time.deltaTime.
        [ItemNotNull][NotNull] private readonly List<_ATask> _m_updateTasks;
        [ItemNotNull][NotNull] private readonly List<_ATask> _m_lateUpdateTasks;
        // Task will run in Update and LateUpdate, use Time.unscaledDeltaTime.
        [ItemNotNull][NotNull] private readonly List<_ATask> _m_unscaledTimeUpdateTasks;
        [ItemNotNull][NotNull] private readonly List<_ATask> _m_unscaledTimeLateUpdateTasks;
        // Task will run in FixedUpdate, use Time.fixedDeltaTime or Time.fixedUnscaledDeltaTime.
        [ItemNotNull][NotNull] private readonly List<_ATask> _m_fixedUpdateTasks;
        [ItemNotNull][NotNull] private readonly List<_ATask> _m_unscaledFixedUpdateTasks;

        // Actually use for foreach traversal lists, to avoid list modification during traversal.
        [ItemNotNull][NotNull] private readonly List<_ATask> _m_taskListForTraversal;
        
        
        private TaskManager()
        {
            _m_updateTasks = new List<_ATask>();
            _m_lateUpdateTasks = new List<_ATask>();

            _m_unscaledTimeUpdateTasks = new List<_ATask>();
            _m_unscaledTimeLateUpdateTasks = new List<_ATask>();

            _m_fixedUpdateTasks = new List<_ATask>();
            _m_unscaledFixedUpdateTasks = new List<_ATask>();

            _m_taskListForTraversal = new List<_ATask>();
        }
        

        /// <summary>
        /// Add a update task.
        /// </summary>
        internal void AddUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_updateTasks)
            {
                _m_updateTasks.Add(_task);
            }
        }
        /// <summary>
        /// Remove a update task.
        /// </summary>
        internal void RemoveUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_updateTasks)
            {
                _m_updateTasks.Remove(_task);
            }
        }
        /// <summary>
        /// Add a LateUpdate task.
        /// </summary>
        internal void AddLateUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_lateUpdateTasks)
            {
                _m_lateUpdateTasks.Add(_task);
            }
        }
        /// <summary>
        /// Remove a LateUpdate task.
        /// </summary>
        internal void RemoveLateUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_lateUpdateTasks)
            {
                _m_lateUpdateTasks.Remove(_task);
            }
        }
        /// <summary>
        /// Add a Update task, task will use Time.unscaledDeltaTime.
        /// </summary>
        internal void AddUnscaledTimeUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_unscaledTimeUpdateTasks)
            {
                _m_unscaledTimeUpdateTasks.Add(_task);
            }
        }
        /// <summary>
        /// Remove a Update task.
        /// </summary>
        internal void RemoveUnscaledTimeUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
                
            lock (_m_unscaledTimeUpdateTasks)
            {
                _m_unscaledTimeUpdateTasks.Remove(_task);
            }
        }
        /// <summary>
        /// Add a LateUpdate task, task will use Time.unscaledDeltaTime.
        /// </summary>
        internal void AddUnscaledTimeLateUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_unscaledTimeLateUpdateTasks)
            {
                _m_unscaledTimeLateUpdateTasks.Add(_task);
            }
        }
        /// <summary>
        /// Remove a LateUpdate task.
        /// </summary>
        internal void RemoveUnscaledTimeLateUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_unscaledTimeLateUpdateTasks)
            {
                _m_unscaledTimeLateUpdateTasks.Remove(_task);
            }
        }
        /// <summary>
        /// Add a FixedUpdate task, task will use Time.fixedDeltaTime.
        /// </summary>
        internal void AddFixedUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_fixedUpdateTasks)
            {
                _m_fixedUpdateTasks.Add(_task);
            }
        }
        /// <summary>
        /// Remove a FixedUpdate task.
        /// </summary>
        internal void RemoveFixedUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_fixedUpdateTasks)
            {
                _m_fixedUpdateTasks.Remove(_task);
            }
        }
        /// <summary>
        /// Add a FixedUpdate task, task will use Time.fixedUnscaledDeltaTime.
        /// </summary>
        internal void AddUnscaledFixedUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;

            lock (_m_unscaledFixedUpdateTasks)
            {
                _m_unscaledFixedUpdateTasks.Add(_task);
            }
        }
        /// <summary>
        /// Remove a FixedUpdate task.
        /// </summary>
        internal void RemoveUnscaledFixedUpdateTask(_ATask _task)
        {
            if (_task == null)
                return;
            
            lock (_m_unscaledFixedUpdateTasks)
            {
                _m_unscaledFixedUpdateTasks.Remove(_task);
            }
        }
        
        
        /// <summary>
        /// Invoke by Unity.
        /// </summary>
        private void Update()
        {
            _m_taskListForTraversal.Clear();
            lock (_m_updateTasks) _m_taskListForTraversal.AddRange(_m_updateTasks);
            foreach (_ATask task in _m_taskListForTraversal)
            {
                task.Deal(Time.deltaTime);
            }
            
            _m_taskListForTraversal.Clear();
            lock (_m_unscaledTimeUpdateTasks) _m_taskListForTraversal.AddRange(_m_unscaledTimeUpdateTasks);
            foreach (_ATask task in _m_taskListForTraversal)
            {
                task.Deal(Time.unscaledDeltaTime);
            }
        }
        /// <summary>
        /// Invoke by Unity
        /// </summary>
        private void FixedUpdate()
        {
            _m_taskListForTraversal.Clear();
            lock (_m_fixedUpdateTasks) _m_taskListForTraversal.AddRange(_m_fixedUpdateTasks);
            foreach (_ATask task in _m_taskListForTraversal)
            {
                task.Deal(Time.fixedDeltaTime);
            }
            
            _m_taskListForTraversal.Clear();
            lock (_m_unscaledFixedUpdateTasks) _m_taskListForTraversal.AddRange(_m_unscaledFixedUpdateTasks);
            foreach (_ATask task in _m_taskListForTraversal)
            {
                task.Deal(Time.fixedUnscaledDeltaTime);
            }
        }
        /// <summary>
        /// Invoke by Unity.
        /// </summary>
        private void LateUpdate()
        {
            _m_taskListForTraversal.Clear();
            lock (_m_lateUpdateTasks) _m_taskListForTraversal.AddRange(_m_lateUpdateTasks);
            foreach (_ATask task in _m_taskListForTraversal)
            {
                task.Deal(Time.deltaTime);
            }
            
            _m_taskListForTraversal.Clear();
            lock (_m_unscaledTimeLateUpdateTasks) _m_taskListForTraversal.AddRange(_m_unscaledTimeLateUpdateTasks);
            foreach (_ATask task in _m_taskListForTraversal)
            {
                task.Deal(Time.unscaledDeltaTime);
            }
        }
    }
}