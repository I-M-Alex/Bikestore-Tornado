SELECT 
    t.name AS table_name,
    c.name AS column_name,
    ty.name AS data_type,
    c.is_nullable AS is_nullable,
    kcu.name AS foreign_key_constraint  
FROM 
    sys.tables t
INNER JOIN 
    sys.columns c ON t.object_id = c.object_id
INNER JOIN 
    sys.types ty ON c.user_type_id = ty.user_type_id
LEFT JOIN 
    sys.foreign_key_columns fkc ON c.object_id = fkc.parent_object_id AND c.column_id = fkc.parent_column_id
LEFT JOIN 
    sys.key_constraints kcu ON fkc.constraint_object_id = kcu.object_id 
WHERE t.name NOT IN ('sysdiagrams')
ORDER BY 
    t.name, c.column_id;