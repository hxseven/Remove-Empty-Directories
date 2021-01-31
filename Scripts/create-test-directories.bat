rem Change active code page to utf-8 (because of special chars)
chcp 65001

mkdir test-dirs\empty-1
mkdir test-dirs\empty-2
mkdir test-dirs\empty-3
mkdir test-dirs\empty-with-child-dirs-1\empty-subdir-1-of-3
mkdir test-dirs\empty-with-child-dirs-1\empty-subdir-2-of-3
mkdir test-dirs\empty-with-child-dirs-1\empty-subdir-3-of-3

mkdir test-dirs\not-empty
echo Test> test-dirs\not-empty\dummy.txt

mkdir test-dirs\empty-4

mkdir test-dirs\deep-nested-empty-dirs\a\b\c\d\e\f
mkdir test-dirs\deep-nested-empty-dirs\a\b2
mkdir test-dirs\deep-nested-empty-dirs\a\b\c2
mkdir test-dirs\deep-nested-empty-dirs\a\b\c3
mkdir test-dirs\deep-nested-empty-dirs\a\b\c\d\e\f2

mkdir test-dirs\empty-hidden-dir
attrib +h test-dirs\empty-hidden-dir /s /d

mkdir test-dirs\empty-hidden-nested-dirs\a\b
mkdir test-dirs\empty-hidden-nested-dirs\a2\b2
mkdir test-dirs\empty-hidden-nested-dirs\a3\b3
attrib +h test-dirs\empty-hidden-nested-dirs /s /d
attrib +h test-dirs\empty-hidden-nested-dirs\* /s /d
attrib +h test-dirs\empty-hidden-nested-dirs\a\* /s /d

mkdir test-dirs\not-empty-hidden-dir
echo Test> test-dirs\not-empty-hidden-dir\dummy.txt

mkdir test-dirs\contains-1-empty-file
type nul > test-dirs\contains-1-empty-file\empty.txt

mkdir test-dirs\contains-3-empty-files
type nul > test-dirs\contains-3-empty-files\empty-1.txt
type nul > test-dirs\contains-3-empty-files\empty-2.txt
type nul > test-dirs\contains-3-empty-files\empty-3.txt

mkdir test-dirs\contains-empty-files-and-non-empty
type nul > test-dirs\contains-empty-files-and-non-empty\empty-1.txt
type nul > test-dirs\contains-empty-files-and-non-empty\empty-2.txt
echo 1 > test-dirs\contains-empty-files-and-non-empty\not-empty.txt

mkdir test-dirs\with-desktop.ini
echo "test" > test-dirs\with-desktop.ini\desktop.ini

mkdir test-dirs\with-Thumbs.db
echo "test" > test-dirs\with-Thumbs.db\Thumbs.db

mkdir test-dirs\with-mac-DS_Store
echo "test" > test-dirs\with-mac-DS_Store\.DS_Store

mkdir test-dirs\with-AppleDouble-files
echo "test" > test-dirs\with-AppleDouble-files\._test1
echo "test" > test-dirs\with-AppleDouble-files\._test2

mkdir "test-dirs\empty-dirs-with-special-chars\with spaces"
mkdir "test-dirs\empty-dirs-with-special-chars\spaces a b c d"
mkdir "test-dirs\empty-dirs-with-special-chars\#test"
mkdir "test-dirs\empty-dirs-with-special-chars\test!"
mkdir "test-dirs\empty-dirs-with-special-chars\     spaces"
mkdir "test-dirs\empty-dirs-with-special-chars\     spaces      "
mkdir "test-dirs\empty-dirs-with-special-chars\!test"
mkdir "test-dirs\empty-dirs-with-special-chars\^test"
mkdir "test-dirs\empty-dirs-with-special-chars\#+!^-_"

mkdir "test-dirs\empty-dirs-with-special-chars\chinese\删除空目录"
mkdir "test-dirs\empty-dirs-with-special-chars\chinese\测试"
mkdir "test-dirs\empty-dirs-with-special-chars\chinese\红"
mkdir "test-dirs\empty-dirs-with-special-chars\chinese\紅"
mkdir "test-dirs\empty-dirs-with-special-chars\chinese\刪除空目錄"
mkdir "test-dirs\empty-dirs-with-special-chars\chinese\去掉"
mkdir "test-dirs\empty-dirs-with-special-chars\chinese\不是空的"
mkdir "test-dirs\empty-dirs-with-special-chars\chinese\这个\空的\是"
echo Test> test-dirs\empty-dirs-with-special-chars\chinese\不是空的\文件
echo Test> test-dirs\empty-dirs-with-special-chars\chinese\不是空的2\文件.txt

mkdir "test-dirs\empty-dirs-with-special-chars\arabic\صباح الخير"
mkdir "test-dirs\empty-dirs-with-special-chars\arabic\اختبار"
mkdir "test-dirs\empty-dirs-with-special-chars\arabic\ر"
mkdir "test-dirs\empty-dirs-with-special-chars\arabic\ليس فارغا"
echo Test> "test-dirs\empty-dirs-with-special-chars\arabic\ليس فارغا\dummy.txt"

mkdir "test-dirs\empty-dirs-with-special-chars\russian\т"
mkdir "test-dirs\empty-dirs-with-special-chars\russian\тест"
mkdir "test-dirs\empty-dirs-with-special-chars\russian\удалить пустые каталоги"
mkdir "test-dirs\empty-dirs-with-special-chars\russian\не пусто"
echo Test> "test-dirs\empty-dirs-with-special-chars\russian\не пусто\да да.txt"

echo "Done, created all test directories"
pause