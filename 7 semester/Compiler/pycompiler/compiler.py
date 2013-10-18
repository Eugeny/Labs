import dis


class Buffer:
    def __init__(self):
        self.order = []

    def __contains__(self, k):
        return k in self.order

    def __getitem__(self, k):
        if not k in self.order:
            self.order.append(k)
        return self.order.index(k)


class PyNone:
    def emit(self, c):
        c.emit_bytes(ord('N'))


class PyFalse:
    def emit(self, c):
        c.emit_bytes(ord('F'))


class PyTrue:
    def emit(self, c):
        c.emit_bytes(ord('T'))


class PyInt:
    def __init__(self, data=0):
        self.data = data

    def emit(self, c):
        c.emit_bytes(ord('i'))
        c.emit_int32(self.data)


class PyFloat:
    def __init__(self, data=0.0):
        self.data = data

    def emit(self, c):
        c.emit_bytes(ord('f'))
        s = str(self.data)
        c.emit_bytes(len(s))
        c.emit_bytes(*(ord(b) for b in s))


class PyTuple:
    def __init__(self, data=[]):
        self.data = data

    def emit(self, c):
        c.emit_bytes(ord('('))
        c.emit_int32(len(self.data))
        for o in self.data:
            o.emit(c)


class PyString:
    def __init__(self, data=''):
        self.data = data

    def emit(self, c):
        c.emit_bytes(ord('s'))
        c.emit_int32(len(self.data))
        c.emit_bytes(*(ord(b) for b in self.data))


class PyOffset (object):
    def __init__(self, delta):
        self.delta = delta


class PythonCompiler (object):
    def __init__(self):
        self.output = []

    def i162b(self, x):
        return [
            (x / (256 ** 0)) & 255,
            (x / (256 ** 1)) & 255,
        ]

    def i2b(self, x):
        return [
            (x / (256 ** 0)) & 255,
            (x / (256 ** 1)) & 255,
            (x / (256 ** 2)) & 255,
            (x / (256 ** 3)) & 255,
        ]

    def emit_bytes(self, *b):
        self.output += b

    def emit_int32(self, *i):
        for x in i:
            self.emit_bytes(*self.i2b(x))

    def emit_int16(self, *i):
        for x in i:
            self.emit_bytes(*self.i162b(x))

    def compile(self, ast):
        self.emit_bytes(0x03, 0xf3, 0x0d, 0x0a, 0x79, 0x64, 0x40, 0x52)
        self.compile_function(ast)

    def compile_function(self, ast):
        all_constants = Buffer()
        all_vars = Buffer()

        self.emit_bytes(ord('c'))
        self.emit_int32(0)  # argcount
        self.emit_int32(0)  # locals
        self.emit_int32(256)  # stacksize
        self.emit_int32(0x40)  # flags

        def emit_opcode(code, oc):
            code.append(dis.opmap[oc])

        def emit_offset(code, d):
            code.append(PyOffset(d))
            code.append(None)

        def compile_expression(exp):
            code = []

            if exp.name == 'variable':
                emit_opcode(code, 'LOAD_NAME')
                code += self.i162b(all_vars[exp.parameters['name']])
            
            elif exp.name == 'integer':
                emit_opcode(code, 'LOAD_CONST')
                code += self.i162b(all_constants[PyInt(exp.parameters['value'])])

            elif exp.name == 'float':
                emit_opcode(code, 'LOAD_CONST')
                code += self.i162b(all_constants[PyFloat(exp.parameters['value'])])

            elif exp.name == 'string':
                emit_opcode(code, 'LOAD_CONST')
                code += self.i162b(all_constants[PyString(exp.parameters['value'])])

            elif exp.name == 'call':
                emit_opcode(code, 'LOAD_NAME')
                code += self.i162b(all_vars[exp.parameters['name']])
                for a in exp.children['args']:
                    code += compile_expression(a)
                emit_opcode(code, 'CALL_FUNCTION')
                code += self.i162b(len(exp.children['args']))

            elif exp.name in ['plus', 'minus', 'multiply', 'divide', 'modulo', 'op-equals', 'op-less']:
                code += compile_expression(exp.children['left'][0])
                code += compile_expression(exp.children['right'][0])
                emit_opcode(code, {
                    'plus': 'BINARY_ADD',
                    'multiply': 'BINARY_MULTIPLY',
                    'minus': 'BINARY_SUBTRACT',
                    'divide': 'BINARY_DIVIDE',
                    'modulo': 'BINARY_MODULO',
                    'op-equals': 'COMPARE_OP',
                    'op-less': 'COMPARE_OP',
                }[exp.name])
                if exp.name in ['op-equals', 'op-less']:
                    code += self.i162b(dis.cmp_op.index({
                        'op-equals': '==',
                        'op-less': '<',
                    }[exp.name]))

            else:
                raise Exception('Unknown expression node: %s' % exp.name)

            return code

        def compile_code_block(ops, ret=False):
            code = []

            for statement in ops:
                if statement.name == 'assignment':
                    code += compile_expression(statement.children['value'][0])
                    emit_opcode(code, 'STORE_NAME')
                    code += self.i162b(all_vars[statement.parameters['target']])
                
                elif statement.name == 'print':
                    code += compile_expression(statement.children['value'][0])
                    emit_opcode(code, 'PRINT_ITEM')
                    emit_opcode(code, 'PRINT_NEWLINE')
                
                elif statement.name == 'if':
                    code_true = compile_code_block(statement.children['code-true'])
                    code_false = compile_code_block(statement.children['code-false'])
                    code += compile_expression(statement.children['condition'][0])
                    emit_opcode(code, 'POP_JUMP_IF_FALSE')
                    emit_offset(code, len(code_true) + 5)
                    code += code_true
                    emit_opcode(code, 'JUMP_FORWARD')
                    code += self.i162b(len(code_false))
                    code += code_false
                
                elif statement.name == 'for':
                    block_code = compile_code_block(statement.children['code'])
                    source_code = compile_expression(statement.children['source'][0])

                    emit_opcode(code, 'SETUP_LOOP')
                    code += self.i162b(len(block_code) + len(source_code) + 11)
        
                    code += source_code
                    
                    emit_opcode(code, 'GET_ITER')

                    emit_opcode(code, 'FOR_ITER')
                    code += self.i162b(len(block_code) + 6)

                    emit_opcode(code, 'STORE_NAME')
                    code += self.i162b(all_vars[statement.parameters['variable']])

                    code += block_code

                    emit_opcode(code, 'JUMP_ABSOLUTE')
                    emit_offset(code, -len(block_code) - 7)

                    emit_opcode(code, 'POP_BLOCK')
                    
                else:
                    raise Exception('Unknown statement node: %s' % statement.name)

            if ret:
                emit_opcode(code, 'LOAD_CONST')
                code += self.i162b(all_constants[PyNone()])
                emit_opcode(code, 'RETURN_VALUE')
            return code
            
        pre_code = compile_code_block(ast.children['content'], ret=True)

        bytecode = []
        for c in pre_code:
            if c is not None:
                if type(c) is PyOffset:
                    bytecode += self.i162b(len(bytecode) + c.delta)
                else:
                    bytecode.append(c)
    
        print pre_code
        print bytecode

        PyString(''.join(chr(c) for c in bytecode)).emit(self)
        
        PyTuple(all_constants.order).emit(self)
        PyTuple([PyString(x) for x in all_vars.order]).emit(self)
        
        PyTuple([]).emit(self)
        PyTuple([]).emit(self)
        PyTuple([]).emit(self)  # ?
        PyString('unknown.py').emit(self)
        PyString(ast.parameters['name']).emit(self)
        self.emit_int32(1)
        PyString('').emit(self)

    def write(self, file):
        file.write(''.join(chr(x) for x in self.output))
