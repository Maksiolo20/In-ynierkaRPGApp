﻿namespace RPGApp.Data
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GameMasterId { get; set; }
        public User GameMaster { get; set; }
        public List<Card> Cards { get; set; }
        public List<Note> Notes { get; set; }
    }
}
