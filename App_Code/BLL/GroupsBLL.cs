using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SwimTeamDatabaseTableAdapters;
using System.Collections.Generic;

[System.ComponentModel.DataObject]
public class GroupsBLL
{
    private GroupsTableAdapter _groupsAdapter = null;
    protected GroupsTableAdapter Adapter
    {
        get
        {
            if (_groupsAdapter == null)
                _groupsAdapter = new GroupsTableAdapter();

            return _groupsAdapter;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.GroupsDataTable GetAllGroups()
    {
        return Adapter.GetAllGroups();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public SwimTeamDatabase.GroupsDataTable GetActiveGroups()
    {
        return Adapter.GetGroupsByActiveStatus(true);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.GroupsDataTable GetInActiveGroups()
    {
        return Adapter.GetGroupsByActiveStatus(false);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public SwimTeamDatabase.GroupsDataTable GetGroupByGroupID(int GroupID)
    {
        return Adapter.GetGroupByGroupID(GroupID);
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public bool UpdateGroup(string GroupName, string Created, bool Active, bool DefaultGroup, int original_GroupID)
    {
        GroupName = GroupName.Trim();
        Created = Created.Trim();

        MakeAnyCurrentDefaultGroupsRegular(DefaultGroup);

        return Adapter.UpdateGroup(GroupName, Created, Active, DefaultGroup, original_GroupID) == 1;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public bool InsertGroup(String GroupName, string Created, bool DefaultGroup)
    {
        GroupName = GroupName.Trim();

        MakeAnyCurrentDefaultGroupsRegular(DefaultGroup);

        return Adapter.InsertGroup(GroupName, Created, true, DefaultGroup) == 1;
    }

    private void MakeAnyCurrentDefaultGroupsRegular(bool DefaultGroup)
    {
        if (DefaultGroup)
        {
            //If this group is being changed to the default group, we need to edit the 
            //previous default group to a regular group
            SwimTeamDatabase.GroupsDataTable groups = Adapter.GetDefaultGroup();
            if (groups.Count != 0)
            {
                SwimTeamDatabase.GroupsRow group = groups[0];
                group.DefaultGroup = false;

                Adapter.Update(group);
            }
        }
    }

    public List<String> GetParentEmailsInGroup(int GroupID)
    {
        List<String> Emails = new List<string>();

        SwimTeamDatabase.ParentsDataTable Parents = new ParentsBLL().GetParents();
        SwimTeamDatabase.SwimmersDataTable Swimmers = new SwimmersBLL().GetSwimmersByGroupID(GroupID);


        foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
        {
            int parents_found = 0;
            for (int i = 0; i < Parents.Count; i++)
            {
                if (Swimmer.FamilyID == Parents[i].FamilyID)
                {
                    if (!String.IsNullOrEmpty(Parents[i].Email))
                        if (!Emails.Contains(Parents[i].Email))
                            Emails.Add(Parents[i].Email);
                    parents_found++;
                    if (parents_found >= 2)
                        i = Parents.Count;
                }
            }
        }

        return Emails;
    }

    public List<String> GetParentAndSwimmerEmailsInGroup(int GroupID)
    {
        List<String> Emails = new List<string>();

        SwimTeamDatabase.ParentsDataTable Parents = new ParentsBLL().GetParents();
        SwimTeamDatabase.SwimmersDataTable Swimmers = new SwimmersBLL().GetSwimmersByGroupID(GroupID);


        foreach (SwimTeamDatabase.SwimmersRow Swimmer in Swimmers)
        {
            if (!String.IsNullOrEmpty(Swimmer.Email))
                if (!Emails.Contains(Swimmer.Email))
                    Emails.Add(Swimmer.Email);

            int parents_found = 0;
            for (int i = 0; i < Parents.Count; i++)
            {
                if (Swimmer.FamilyID == Parents[i].FamilyID)
                {
                    if (!Emails.Contains(Parents[i].Email))
                        Emails.Add(Parents[i].Email);
                    parents_found++;
                    if (parents_found >= 2)
                        i = Parents.Count;
                }
            }
        }

        return Emails;
    }
}