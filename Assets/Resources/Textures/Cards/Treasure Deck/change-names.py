import os

i = 0
for name in os.listdir():
    if ".jpg" in name and not ".meta" in name:
        prefix = name.split(".jpg")[0]
        firstToChange = prefix + ".jpg"
        secondToChange = prefix + ".jpg.meta"

        newFirst = str(i) + ".jpg"
        newSecond = str(i) + ".jpg.meta"

        os.rename(firstToChange, newFirst)
        os.rename(secondToChange, newSecond)

        i += 1
