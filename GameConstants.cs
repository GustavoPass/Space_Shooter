using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceShooter {
    public class GameConstants {

        // Tags
        public const string PLAYER_TAG                 = "Player";
        public const string PLAYER_BULLET_TAG          = "PlayerBullet";
        public const string BORDER_TAG                 = "Border";
        public const string ENEMY_TAG                  = "Enemy";
        public const string ENEMY_BULLET_TAG           = "EnemyBullet";
        public const string SPECIAL_COLLECT_TAG = "SpecialCollect";
		public const string SPECIAL_ENEMY_TAG = "SpecialEnemy";
		public const string ENEMY_SHIELD_TAG = "Shield";
		public const string	MINI_BOSS_TAG = "MiniBoss";
		public const string BOLINHA_TAG = "Bolinhas";
		public const string BOSS_TAG = "Boss";
		public const string ASTEROID_TAG = "Asteroid";


        // Speed Values
		public const float BACKGROUND_ROLL_SPEED = 0.5F;
        public const float ENEMY1_SPEED          = 2F;
		public const float ENEMY2_SPEED          = 5F;
		public const float ASTEROID_SPEED        = 5000F;

        // Times 
        public const float PLAYER_SHOOT_INTERVAl             = 0.15F;
        public const float PLAYER_SPECIAL_INTERVAL           = 0.1F;
        public const float PLAYER_SPECIAL_COOL_DOWN_INTERVAL = 2F;
        public const float ENEMEY1_MIN_FIRE_INTERVAL         = 0.3F;
        public const float ENEMEY1_MAX_FIRE_INTERVAL         = 1F;

        // Number of Bullets
        public const int PLAYER_BULLET_NUMBER = 30;
        public const int ENEMY1_BULLET_NUMBER = 50;

        // Attack Values
        public const float PLAYER_BULLET_FORCE                   = 500F;
        public const float PLAYER_SPECIAL_FORCE                  = 500F;

        public const int   PLAYER_SPECIAL_NUMBER_OF_SINGLE_SHOTS = 1;
        public const int   PLAYER_SPECIAL_NUMBER_OF_WAVES        = 1;
        public const float PLAYER_SPECIAL_ANGLE                  = 360F / 8f;
        public const float PLAYER_SPECIAL_START_ANGLE            = 90F;
        public const float PLAYER_SPECIAL_RADIUS                 = 0.5F;
        public const float ENEMY1_BULLET_FORCE                   = 400;

		public const int EXPLOSION_NUMBER = 10;

    }
}
