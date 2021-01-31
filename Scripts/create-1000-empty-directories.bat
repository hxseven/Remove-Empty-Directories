mkdir test-1000-dirs

FOR /L %%A IN (1,1,1000) DO (
  mkdir test-1000-dirs\dir-%%A
)

echo "Done, created 1000 empty directories"
pause