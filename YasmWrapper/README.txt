Yasmwrapper is used to run vsyasm.exe needed to build GMP assembly (.asm) files.

By default visual studio passes an array of a long list of assembly files to vsyasm to build.

```

"[path-to-vsyasm.exe\"vsyasm.exe -Xvc -f x64 -g cv8 -o "[path to output directory]" -rraw -pgas  "[file-name-1].asm"  "[file-name-2].asm"   "[file-name-n].asm" 

```

Unforunately this causes vsyasm.exe to throw an unhandled expection. After experimentation 
vsyasm could build the assembly files if they were passed one at a time.

```

"[path-to-vsyasm.exe\"vsyasm.exe -Xvc -f x64 -g cv8 -o "[path to output directory]" -rraw -pgas "[file-name-1].asm"
"[path-to-vsyasm.exe\"vsyasm.exe -Xvc -f x64 -g cv8 -o "[path to output directory]" -rraw -pgas "[file-name-2].asm" 
...
"[path-to-vsyasm.exe\"vsyasm.exe -Xvc -f x64 -g cv8 -o "[path to output directory]" -rraw -pgas "[file-name-n].asm" 

```

After trial an error I built YasmWrapper which takes the arguments Visual studio passes to it, as shown in the first example,
and then executes vsyasm.exe for each assembly file.

If you prefer, you can download the latest [vsyasm.exe](http://www.tortall.net/projects/yasm/releases/) and attempt to get the build working.

It appears [MPIR builds](https://github.com/wbhart/mpir/blob/master/appveyor.yml) builds fine on linux using the latest yasm.exe so not sure what the issue with vsyasm.exe is.