import sqlite3

connection_string = "dnd10k.db"
effects_file = "effects.txt"

f = open(effects_file, "rt")
connection = sqlite3.connect(connection_string)
cursor = connection.cursor()

create_table = """
    create table if not exists MagicalEffects (
        ID integer not null primary key,
        Description text not null);
 """

delete_existing = """
delete from MagicalEffects
"""

insert_effect = """
    insert into MagicalEffects(Description) 
    values(?);
"""

cursor.execute(create_table)
cursor.execute(delete_existing)

effect_lines = f.readlines() 

effects = [(e[5:-1],) for e in effect_lines]

cursor.executemany(insert_effect,effects)

connection.commit()
connection.close()
f.close()
