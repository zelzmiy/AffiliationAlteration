using COTL_API.CustomFollowerCommand;
using ForceFriendship.Helpers;
using Lamb.UI.FollowerSelect;
using src.Extensions;
using src.UI.Menus;
using System;
using System.Collections.Generic;
using System.IO;

namespace ForceFriendship.Commands
{
    internal class BefriendCommand : CustomFollowerCommand
    {
        public override string InternalName => "Make_Friends";
        public override List<FollowerCommandCategory> Categories { get; } = new() { FollowerCommandCategory.MAKE_DEMAND_COMMAND };
        public override string GetTitle(Follower follower) { return "Befriend"; }
        public override string GetDescription(Follower follower) { return $"Force the {follower} to become friends with someone"; }
        public override Sprite CommandIcon => TextureHelper.CreateSpriteFromPath(
            Path.Combine(Plugin.PluginPath, "Assets", "friendship.png"));

        private readonly int _levelOfFriendship = 5;

        public override void Execute(interaction_FollowerInteraction interaction, FollowerCommands finalCommand)
        {
            ChangeRelationship(IDAndRelationship.RelationshipState.Friends, _levelOfFriendship, interaction.follower, "Select follower to befriend");
            base.Execute(interaction, finalCommand);
        }

        public override bool ShouldAppearFor(Follower follower)
        {
            return follower.IsSleeping();
        }
    }
}
