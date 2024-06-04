using COTL_API.CustomFollowerCommand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ForceFriendship.Commands
{
    internal class DismayCommand : CustomFollowerCommand
    {
        public override string InternalName => "Make_Enemies";

        public override List<FollowerCommandCategory> Categories { get; } = new() { FollowerCommandCategory.MAKE_DEMAND_COMMAND };
        public override string GetTitle(Follower follower) { return "Dismay"; }
        public override string GetDescription(Follower follower) { return $"Force the {follower} to hate someone"; }
        public override Sprite CommandIcon => TextureHelper.CreateSpriteFromPath(
            Path.Combine(Plugin.PluginPath, "Assets", "dismay.png"));

        private readonly int _levelOfFriendship = -10;

        public override void Execute(interaction_FollowerInteraction interaction, FollowerCommands finalCommand)
        {
            ChangeRelationship(IDAndRelationship.RelationshipState.Enemies, _levelOfFriendship, interaction.follower, "Select follower to be hated");
            base.Execute(interaction, finalCommand);
        }

        public override bool ShouldAppearFor(Follower follower)
        {
            return follower.IsSleeping();
        }
    }
}
