using COTL_API.CustomFollowerCommand;
using System.Collections.Generic;
using System.IO;

namespace ForceFriendship.Commands
{
    internal class EstrangeCommand : CustomFollowerCommand
    {
        public override string InternalName => "Make_Strangers";

        public override List<FollowerCommandCategory> Categories { get; } = new() { FollowerCommandCategory.MAKE_DEMAND_COMMAND };
        public override string GetTitle(Follower follower) { return "Estrange"; }
        public override string GetDescription(Follower follower) { return $"Force the {follower} to forget someone"; }
        public override Sprite CommandIcon => TextureHelper.CreateSpriteFromPath(
            Path.Combine(Plugin.PluginPath, "Assets", "neutral.png"));

        private readonly int _levelOfFriendship = 0;

        public override void Execute(interaction_FollowerInteraction interaction, FollowerCommands finalCommand)
        {
            ChangeRelationship(IDAndRelationship.RelationshipState.Strangers, _levelOfFriendship, interaction.follower, "Select follower to be forgotten");
            base.Execute(interaction, finalCommand);
        }

        public override bool ShouldAppearFor(Follower follower)
        {
            return follower.IsSleeping();
        }
    }
}
