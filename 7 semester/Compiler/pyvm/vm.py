import dis
from random import random


class VM (object):
    def __init__(self):
        pass

    def execute(self, code):
        _locals = {}
        _stack = []
        _consts = code.co_consts
        _names = code.co_names
        _builtins = {
            'range': range,
            'random': random,
        }
        bytecode = list(ord(x) for x in code.co_code)
        self.ip = 0

        def push(x):
            _stack.append(x)

        def pop():
            return _stack.pop(-1)

        def eval_name(x):
            if x in _locals:
                return _locals[x]
            elif x in _builtins:
                return _builtins[x]

        def consume_byte():
            self.ip += 1
            return bytecode[self.ip - 1]

        def consume_int16():
            self.ip += 2
            return bytecode[self.ip - 2] + bytecode[self.ip - 1] * 256
        
        def jump(addr):
            self.ip = addr

        def log_op(opcode, *args):
            for k in dis.opmap:
                if dis.opmap[k] == opcode:
                    #print ' :: ', k, args
                    pass

        while self.ip < len(bytecode):
            op = consume_byte()
            if op == dis.opmap['LOAD_NAME']:
                arg = consume_int16()
                log_op(op, arg)
                push(eval_name(_names[arg]))
            elif op == dis.opmap['LOAD_CONST']:
                arg = consume_int16()
                log_op(op, arg)
                push(_consts[arg])
            elif op == dis.opmap['CALL_FUNCTION']:
                argc = consume_int16()
                args = list(reversed([pop() for _ in range(argc)]))
                fx = pop()
                log_op(op, fx, args)
                push(fx(*args))
            elif op == dis.opmap['STORE_NAME']:
                arg = consume_int16()
                log_op(op, arg)
                _locals[_names[arg]] = pop()
            elif op == dis.opmap['SETUP_LOOP']:
                arg = consume_int16()
                log_op(op, arg)
            elif op == dis.opmap['GET_ITER']:
                log_op(op)
                push(iter(pop()))
            elif op == dis.opmap['FOR_ITER']:
                arg = consume_int16()
                log_op(op, arg)
                i = pop()
                try:
                    v = i.next()
                    push(i)
                    push(v)
                except StopIteration:
                    jump(self.ip + arg)
            elif op == dis.opmap['PRINT_ITEM']:
                log_op(op)
                print pop(),
            elif op == dis.opmap['PRINT_NEWLINE']:
                log_op(op)
                print
            elif op == dis.opmap['BINARY_MODULO']:
                r, l = pop(), pop()
                log_op(op, l, r)
                push(l % r)
            elif op == dis.opmap['BINARY_ADD']:
                r, l = pop(), pop()
                log_op(op, l, r)
                push(l + r)
            elif op == dis.opmap['BINARY_SUBTRACT']:
                r, l = pop(), pop()
                log_op(op, l, r)
                push(l - r)
            elif op == dis.opmap['BINARY_MULTIPLY']:
                r, l = pop(), pop()
                log_op(op, l, r)
                push(l * r)
            elif op == dis.opmap['BINARY_DIVIDE']:
                r, l = pop(), pop()
                log_op(op, l, r)
                push(l / r)
            elif op == dis.opmap['COMPARE_OP']:
                arg = consume_int16()
                r, l = pop(), pop()
                log_op(op, arg, l, r)
                if dis.cmp_op[arg] == '==':
                    push(l == r)
                elif dis.cmp_op[arg] == '<':
                    push(l < r)
                else:
                    print 'Unknown comparison', dis.cmp_op[arg]
                    return
            elif op == dis.opmap['POP_JUMP_IF_FALSE']:
                arg = consume_int16()
                val = pop()
                if not val:
                    jump(arg)
                log_op(op, arg)
            elif op == dis.opmap['JUMP_ABSOLUTE']:
                arg = consume_int16()
                jump(arg)
                log_op(op, arg)
            elif op == dis.opmap['JUMP_FORWARD']:
                arg = consume_int16()
                jump(self.ip + arg)
                log_op(op, arg)
            elif op == dis.opmap['POP_BLOCK']:
                log_op(op)
            elif op == dis.opmap['RETURN_VALUE']:
                log_op(op)
            else:
                log_op(op)
                print 'Unknown opcode'
                return
