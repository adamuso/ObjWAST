
(module
  (start $__init)
  (import "objwast" "STACK_SIZE" (global $__stackSize i32))
  (memory 1)

  (func $__init
    i32.const 0
    i32.load 
    drop
    
    ;; if i32.eqz 
    ;;   i32.const 0
    ;;   get_global $__stackSize
    ;;   i32.store
    ;; end
  )


  (func $__push_i32 (param $value i32) (local $i i32)
    
    (tee_local $i (i32.add (i32.load (i32.const 0)) (i32.const -4) )) ;; i = mem[0] - 4
    
    (if (i32.lt_s (get_local $i) (i32.const 0)) ;; if (i < 0)
      (then
        ;; throw new StackOverflowException()
      )
    )
    
    get_local $value          ;; [i, value]
    i32.store                 ;; []
                              ;; mem[i] = value
    (i32.store (i32.const 0) (get_local $i)) ;; mem[0] = i
  )

  (func $__pop_i32 (result i32) (local $i i32)
    
    (i32.load (tee_local $i (i32.load (i32.const 0)) )) ;; [value], i = mem[0] 
    i32.const 0 ;; [value, 0]
    (tee_local $i (i32.add (get_local $i) (i32.const 4) )) ;; [value, 0, i], i += 4
    
    (if (i32.gt_s (get_local $i) (get_global $__stackSize)) ;; i > STACK_SIZE
      (then
        ;; throw new StackUnderflowException()
      )
    )

    i32.store ;; [value]
  )
)