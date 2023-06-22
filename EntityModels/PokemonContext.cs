using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace testAPI.EntityModels;

public partial class PokemonContext : DbContext
{
    public PokemonContext()
    {
    }

    public PokemonContext(DbContextOptions<PokemonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ability> Abilities { get; set; }

    public virtual DbSet<AllPokemonAbilitiesNamesView> AllPokemonAbilitiesNamesViews { get; set; }

    public virtual DbSet<AllTrainersPokemonTeam> AllTrainersPokemonTeams { get; set; }

    public virtual DbSet<EvoListView> EvoListViews { get; set; }

    public virtual DbSet<Evolution> Evolutions { get; set; }

    public virtual DbSet<Move> Moves { get; set; }

    public virtual DbSet<Pokemon> Pokemons { get; set; }

    public virtual DbSet<PokemonMovesView> PokemonMovesViews { get; set; }

    public virtual DbSet<PokemonTypesView> PokemonTypesViews { get; set; }

    public virtual DbSet<Sprite> Sprites { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<TrainersPokemon> TrainersPokemons { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=54320;Database=pokemon;Username=postgres;Password=my_password");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("abilities_pkey");

            entity.ToTable("abilities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ability1)
                .HasMaxLength(32)
                .HasColumnName("ability");
        });

        modelBuilder.Entity<AllPokemonAbilitiesNamesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("all_pokemon_abilities_names_view");

            entity.Property(e => e.AbilityName)
                .HasMaxLength(32)
                .HasColumnName("ability_name");
            entity.Property(e => e.PokemonName)
                .HasMaxLength(64)
                .HasColumnName("pokemon_name");
        });

        modelBuilder.Entity<AllTrainersPokemonTeam>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("all_trainers_pokemon_team");

            entity.Property(e => e.IsShiny).HasColumnName("is_shiny");
            entity.Property(e => e.PokemonName)
                .HasMaxLength(64)
                .HasColumnName("pokemon_name");
            entity.Property(e => e.PokemonSpecies)
                .HasMaxLength(64)
                .HasColumnName("pokemon_species");
            entity.Property(e => e.TrainerName)
                .HasMaxLength(200)
                .HasColumnName("trainer_name");
        });

        modelBuilder.Entity<EvoListView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("evo_list_view");

            entity.Property(e => e.EvolvesTo)
                .HasMaxLength(64)
                .HasColumnName("evolves_to");
            entity.Property(e => e.Pokemon)
                .HasMaxLength(64)
                .HasColumnName("pokemon");
            entity.Property(e => e.Trigger)
                .HasMaxLength(128)
                .HasColumnName("trigger");
        });

        modelBuilder.Entity<Evolution>(entity =>
        {
            entity.HasKey(e => new { e.PokemonId, e.EvolvesTo }).HasName("evolutions_pkey");

            entity.ToTable("evolutions");

            entity.Property(e => e.PokemonId).HasColumnName("pokemon_id");
            entity.Property(e => e.EvolvesTo).HasColumnName("evolves_to");
            entity.Property(e => e.Trigger)
                .HasMaxLength(128)
                .HasColumnName("trigger");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.Evolutions)
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pokemon_by_pokemon");
        });

        modelBuilder.Entity<Move>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("moves_pkey");

            entity.ToTable("moves");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Move1)
                .HasMaxLength(32)
                .HasColumnName("move");
        });

        modelBuilder.Entity<Pokemon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pokemon_pkey");

            entity.ToTable("pokemon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accuracy)
                .HasPrecision(4, 2)
                .HasColumnName("accuracy");
            entity.Property(e => e.Attack).HasColumnName("attack");
            entity.Property(e => e.BaseXp).HasColumnName("base_xp");
            entity.Property(e => e.Defense).HasColumnName("defense");
            entity.Property(e => e.Evasion)
                .HasPrecision(4, 2)
                .HasColumnName("evasion");
            entity.Property(e => e.GenerationId).HasColumnName("generation_id");
            entity.Property(e => e.Height)
                .HasPrecision(7, 2)
                .HasColumnName("height");
            entity.Property(e => e.Hp).HasColumnName("hp");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.SpecialAttack).HasColumnName("special_attack");
            entity.Property(e => e.SpecialDefense).HasColumnName("special_defense");
            entity.Property(e => e.Speed).HasColumnName("speed");
            entity.Property(e => e.Weight)
                .HasPrecision(7, 2)
                .HasColumnName("weight");

            entity.HasMany(d => d.Abilities).WithMany(p => p.Pokemons)
                .UsingEntity<Dictionary<string, object>>(
                    "PokemonAbilitiesXref",
                    r => r.HasOne<Ability>().WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_ability_id_abilities"),
                    l => l.HasOne<Pokemon>().WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pokemon_id_pokemon"),
                    j =>
                    {
                        j.HasKey("PokemonId", "AbilityId").HasName("pokemon_abilities_xref_pkey");
                        j.ToTable("pokemon_abilities_xref");
                        j.IndexerProperty<int>("PokemonId").HasColumnName("pokemon_id");
                        j.IndexerProperty<int>("AbilityId").HasColumnName("ability_id");
                    });

            entity.HasMany(d => d.Moves).WithMany(p => p.Pokemons)
                .UsingEntity<Dictionary<string, object>>(
                    "PokemonMovesXref",
                    r => r.HasOne<Move>().WithMany()
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_move_id_moves"),
                    l => l.HasOne<Pokemon>().WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pokemon_id_pokemon"),
                    j =>
                    {
                        j.HasKey("PokemonId", "MoveId").HasName("pokemon_moves_xref_pkey");
                        j.ToTable("pokemon_moves_xref");
                        j.IndexerProperty<int>("PokemonId").HasColumnName("pokemon_id");
                        j.IndexerProperty<int>("MoveId").HasColumnName("move_id");
                    });
        });

        modelBuilder.Entity<PokemonMovesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("pokemon_moves_view");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Move)
                .HasMaxLength(32)
                .HasColumnName("move");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PokemonTypesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("pokemon_types_view");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(32)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Sprite>(entity =>
        {
            entity.HasKey(e => e.PokemonId).HasName("sprites_pkey");

            entity.ToTable("sprites");

            entity.Property(e => e.PokemonId)
                .ValueGeneratedNever()
                .HasColumnName("pokemon_id");
            entity.Property(e => e.Normal)
                .HasMaxLength(250)
                .HasColumnName("normal");
            entity.Property(e => e.Shiny)
                .HasMaxLength(250)
                .HasColumnName("shiny");

            entity.HasOne(d => d.Pokemon).WithOne(p => p.Sprite)
                .HasForeignKey<Sprite>(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pokemon_id_pokemon");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trainers_pkey");

            entity.ToTable("trainers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TrainersPokemon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trainers_pokemon_pkey");

            entity.ToTable("trainers_pokemon");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('pokemon_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Exp).HasColumnName("exp");
            entity.Property(e => e.IsShiny).HasColumnName("is_shiny");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Nickname)
                .HasMaxLength(64)
                .HasColumnName("nickname");
            entity.Property(e => e.PokemonId).HasColumnName("pokemon_id");
            entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

            entity.HasOne(d => d.Pokemon).WithMany(p => p.TrainersPokemons)
                .HasForeignKey(d => d.PokemonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pokemon_id_pokemon");

            entity.HasOne(d => d.Trainer).WithMany(p => p.TrainersPokemons)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_trainer_id_traines");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("types_pkey");

            entity.ToTable("types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type1)
                .HasMaxLength(32)
                .HasColumnName("type");

            entity.HasMany(d => d.Pokemons).WithMany(p => p.Types)
                .UsingEntity<Dictionary<string, object>>(
                    "PokemonTypesXref",
                    r => r.HasOne<Pokemon>().WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_pokemon_id_pokemon"),
                    l => l.HasOne<Type>().WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_type_id_types"),
                    j =>
                    {
                        j.HasKey("TypeId", "PokemonId").HasName("pokemon_types_xref_pkey");
                        j.ToTable("pokemon_types_xref");
                        j.IndexerProperty<int>("TypeId").HasColumnName("type_id");
                        j.IndexerProperty<int>("PokemonId").HasColumnName("pokemon_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
