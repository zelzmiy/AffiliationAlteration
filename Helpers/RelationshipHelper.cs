using Lamb.UI.FollowerSelect;
using src.Extensions;
using System;
using System.Collections.Generic;
using static IDAndRelationship;

namespace ForceFriendship.Helpers
{
    static class RelationshipHelper
    {

        public static void ChangeRelationship(RelationshipState state, int relationship, Follower follower, string HeaderText = "")
        {
            // Create follower list 
            Time.timeScale = 0f;
            Follower partner = null;
            List<FollowerSelectEntry> followers = [];
            foreach (Follower cultist in Follower.Followers)
            {
                if (!DataManager.Instance.Followers_Recruit.Contains(cultist.Brain._directInfoAccess) &&
                    cultist.Brain.Info.ID != follower.Brain.Info.ID)
                {
                    followers.Add(new FollowerSelectEntry(cultist, FollowerManager.GetFollowerAvailabilityStatus(cultist.Brain, false)));
                }
            }

            // Create follower selection menu 
            var menu = UIManager.Instance.FollowerSelectMenuTemplate.Instantiate();
            menu.VotingType = TwitchVoting.VotingType.MATING;
            menu.SetHeaderText(HeaderText);

            // Show follower selection menu
            menu.Show(followers, false, true, true, false);

            menu.OnFollowerSelected = (Action<FollowerInfo>)Delegate.Combine(
                menu.OnFollowerSelected,
                new Action<FollowerInfo>(delegate (FollowerInfo followerInfo)
                {
                    partner = FollowerManager.FindFollowerByID(followerInfo.ID);
                    LogInfo($"Succesfully selected follower {partner.name}");

                    IDAndRelationship ship = follower.Brain.Info.GetOrCreateRelationship(partner.Brain.Info.ID);
                    ship.CurrentRelationshipState = state;
                    ship.Relationship = relationship;
                    // not sure i have to this? 
                    IDAndRelationship relationship1 = partner.Brain.Info.GetOrCreateRelationship(follower.Brain.Info.ID);
                    relationship1.CurrentRelationshipState = state;
                    relationship1.Relationship = relationship;
                    LogInfo($"Succesfully Edited the relationship!");

                    CreateRelationshipNotification(state, follower, partner);

                    return;

                }));

            menu.OnHidden = (Action)Delegate.Combine(menu.OnHidden, (Action)delegate
            {
                Time.timeScale = 1f;
                return;
            });
            menu.OnCancel = (Action)Delegate.Combine(menu.OnCancel, (Action)delegate
            {
                GameManager.GetInstance().OnConversationEnd();
                return;
            });
        }

        private static void CreateRelationshipNotification(RelationshipState state, Follower follower1, Follower follower2)
        {
            switch (state)
            { 
                case RelationshipState.Lovers:
                    NotificationCentre.Instance.PlayGenericNotification($"{follower1.Brain.Info.Name} and {follower2.Brain.Info.Name} have been made lovers!", NotificationBase.Flair.Positive);
                    break;

                case RelationshipState.Friends:
                    NotificationCentre.Instance.PlayGenericNotification($"{follower1.Brain.Info.Name} and {follower2.Brain.Info.Name} have been made friends!", NotificationBase.Flair.Positive);
                    break;

                case RelationshipState.Strangers:
                    NotificationCentre.Instance.PlayGenericNotification($"{follower1.Brain.Info.Name} and {follower2.Brain.Info.Name} have been made strangers!", NotificationBase.Flair.None);
                    break;

                case RelationshipState.Enemies:
                    NotificationCentre.Instance.PlayGenericNotification($"{follower1.Brain.Info.Name} and {follower2.Brain.Info.Name} have been made Enemies!", NotificationBase.Flair.Negative);
                    break;
            }
        }
    }
}
