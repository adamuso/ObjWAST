# Module definition



# Structures

## Definition

```
(struct Test
	(field $a i32)
	(field $b i32)
	(field $c f32)
)
```

## Use structure as types

```
(func $main (result i32) (local $nazwa Test) 	
	get_local $nazwa.$a	
	get_local $nazwa.$b 
	get_local $nazwa 
	i32.add
)

;; generates 

(func $main (result i32) (local i32 i32 f32)
 	get_local 0
	get_local 1

	get_local 0
	get_local 1
	get_local 2
)
```

## Methods

```
struct StructureStack
{
	i32 stackSize; ;; stack size in KB
	i32 stack[stackSize * 1024]; ;; stack
};

(struct Test
	(field $a i32)
	(field $b i32)
	(field $c f32)
)

(func $main (result i32) 
			(local $nazwa Test) ;; generates (local i32 i32 f32)
	
	get_local $nazwa.$a	;; generates get_local 0
	get_local $nazwa.$b ;; generates get_local 0
	get_local $nazwa 
	;; generates
	;; get_local 0
	;; get_local 1
	;; get_local 2
	i32.add
)
```
# Structure with props

```
(struct Test2
	(field $a i32) ;; by default it is private
	(field private $b i32)
	(field public $c f32)

	(prop $D i32 (get public) (set private)) ;; auto property

	;; transpiles to 
	(field private $D i32)
	
	(func $__set_D (param i32 i32 f32 i32) (param i32)
	 	get_local 3	
	 	set_local 0
	)
	(func $__get_D (param i32 i32 f32 i32) (result i32)
		get_local 0
	)
	;; ------

	(prop $A i32
		(get public
			get_local $this.$a
		)
		(set private
			get_local $value
			set_local $this.$a
		)	
	)

	;; transpiles to
	(func $__set_A (param i32 i32 f32 i32) (param i32)
	 	get_local 3	
	 	set_local 0
	)
	(func $__get_A (param i32 i32 f32 i32) (result i32)
		get_local 0
	)
	;; -------

	(func $getB (result i32)
		get_local $this.$b
	)

	;; transpiles to
	(func $getB (param i32 i32 f32) (result i32)
		get_local 1
	)
	;; -------
)
```