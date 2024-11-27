// Copyright (c) 2024 Coda
// 
// This file is part of CodaGame, licensed under the MIT License.
// See the LICENSE file in the project root for license information.

using JetBrains.Annotations;

namespace UnityGameFramework.Base
{
    /// <summary>
    /// A task container.
    /// </summary>
    /// <remarks>
    /// <para>A task container handles adding, removing, and dealing with tasks.</para>
    /// </remarks>
    internal abstract class _ATaskContainer<T_TASK> 
        where T_TASK : _ABaseTask
    {
        internal abstract void AddTask([NotNull] T_TASK _task);
        internal abstract void RemoveTask([NotNull] T_TASK _task);
        /// <summary>
        /// Deal tasks.
        /// </summary>
        /// <remarks>
        /// <para>Deal the tasks which need to be dealt in this frame.</para>
        /// <para>Return true if there are tasks to deal, otherwise return false.</para>
        /// </remarks>
        internal abstract bool DealTasks();
        /// <summary>
        /// Move to the next frame.
        /// </summary>
        /// <remarks>
        /// <para>Should be called after DealTasks, to prepare for the next frame deal.</para>
        /// </remarks>
        internal abstract void NextFrame();
    }
}