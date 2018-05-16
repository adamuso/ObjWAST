(module
	(struct MyStruct
		(field private $a i32)
		(field private $b i32)
		(field private $c i32)
		(field private $d i32)
	)
	(func $test (local MyStruct) (result i32)
		i32.const 10
		get_local 0
		i32.add
		i32.const 5
		(i32.sub (i32.mul (i32.const 2)))
		(i32.add (i32.const 5) (i32.const 6))
		i32.add
	)
)