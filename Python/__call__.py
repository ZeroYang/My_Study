class Next:
    list = []
    def __init__(self,low,high):
        for Num in range(low,high):
            self.list.append(Num**2)
            print Num**2

    def __call__(self,Nu):
        return self.list[Nu]

#b = Next(1,6)
#print b.list
#print b(2)


class strtest:
    def __init__(self):
        print "init: this is only test"
    def str(self,arg):
        return "str: this is only test" + arg

if __name__ == "__main__":
    st = strtest()
    print st.str("test")
