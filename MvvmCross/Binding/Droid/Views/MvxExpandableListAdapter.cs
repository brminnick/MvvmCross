using System;
using System.Collections;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Binding.Droid.BindingContext;

namespace MvvmCross.Binding.Droid.Views
{
    public class MvxExpandableListAdapter : MvxAdapter, IExpandableListAdapter
    {
        public MvxExpandableListAdapter(Context context)
            : base(context)
        { }
        
        public MvxExpandableListAdapter(Context context, IMvxAndroidBindingContext bindingContext)
            : base(context, bindingContext)
        { }

        protected MvxExpandableListAdapter(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        private int _groupTemplateId;

        public int GroupTemplateId
        {
            get { return this._groupTemplateId; }
            set
            {
                if (this._groupTemplateId == value)
                    return;

                this._groupTemplateId = value;
                // since the template has changed then let's force the list to redisplay by firing NotifyDataSetChanged()
                if (this.ItemsSource != null)
                    this.NotifyDataSetChanged();
            }
        }

        public int GroupCount => this.Count;

        public virtual void OnGroupExpanded(int groupPosition)
        {
            // do nothing
        }

        public virtual void OnGroupCollapsed(int groupPosition)
        {
            // do nothing
        }

        public virtual bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

        public virtual View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var item = this.GetRawGroup(groupPosition);
            return this.GetBindableView(convertView, item, this.GroupTemplateId);
        }

        public virtual long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public virtual Java.Lang.Object GetGroup(int groupPosition)
        {
            return null;
        }

        // Base implementation returns a long (from BaseExpandableListAdapter.java):
        // bit 0: Whether this ID points to a child (unset) or group (set), so for this method
        //        this bit will be 1.
        // bit 1-31: Lower 31 bits of the groupId
        // bit 32-63: Lower 32 bits of the childId.
        public virtual long GetCombinedGroupId(long groupId)
        {
            return (groupId & 0x7FFFFFFF) << 32;
        }

        // Base implementation returns a long:
        // bit 0: Whether this ID points to a child (unset) or group (set), so for this method
        //        this bit will be 0.
        // bit 1-31: Lower 31 bits of the groupId
        // bit 32-63: Lower 32 bits of the childId.
        public virtual long GetCombinedChildId(long groupId, long childId)
        {
            return (long)(0x8000000000000000UL | (ulong)((groupId & 0x7FFFFFFF) << 32) | (ulong)(childId & 0xFFFFFFFF));
        }

        public virtual object GetRawItem(int groupPosition, int position)
        {
            return ((IEnumerable)this.GetRawGroup(groupPosition)).ElementAt(position);
        }

        public virtual object GetRawGroup(int groupPosition)
        {
            return this.GetRawItem(groupPosition);
        }

        public virtual View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var item = this.GetRawItem(groupPosition, childPosition);

            return this.GetBindableView(convertView, item, this.ItemTemplateId);
        }

        public virtual int GetChildrenCount(int groupPosition)
        {
            return ((IEnumerable)this.GetRawGroup(groupPosition)).Count();
        }

        public virtual long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public virtual Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public virtual Tuple<int, int> GetPositions(object childItem)
        {
            int groupCount = this.Count;

            for (int groupPosition = 0; groupPosition < groupCount; groupPosition++)
            {
                int childPosition = ((IEnumerable)this.GetRawGroup(groupPosition)).GetPosition(childItem);
                if (childPosition != -1)
                    return new Tuple<int, int>(groupPosition, childPosition);

                groupPosition++;
            }

            return null;
        }
    }
}