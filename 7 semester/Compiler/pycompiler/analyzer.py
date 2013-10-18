import re


class ASTNode (object):
    def __init__(self, name):
        self.name = name
        self.parameters = {}
        self.children = {}

    def to_string(self):
        r = '** [%s]\n' % self.name.upper()

        def indent(s):
            return ''.join(('    ' + l + '\n') for l in s.splitlines())

        for k, v in self.parameters.iteritems():
            r += '  %s = %s\n' % (k, repr(v))

        for k, v in self.children.iteritems():
            print self.name,k,v
            r += ' - %s:\n%s\n' % (k, '\n'.join(indent(c.to_string()) for c in v))

        return r.strip(' \n')


class LeaveBlock (Exception):
    pass


class UnknownSyntax (Exception):
    pass


class Analyzer (object):
    def __init__(self, tokens):
        self.tokens = tokens
        self.short = ''.join(t.short for t in tokens)
        print repr(self.short)

    def run(self):
        root = ASTNode('function')
        root.parameters['name'] = '<module>'
        root.children['content'] = self.process_codeblock()
        return root

    def _peek(self):
        return self.tokens[0]

    def _consume(self, amount):
        r = self.tokens[:amount]
        self.tokens = self.tokens[amount:]
        self.short = self.short[amount:]
        return r

    def _match(self, rx):
        return re.match('^' + rx, self.short)

    def process_codeblock(self):
        nodes = []
        while True:
            if not self.tokens:
                return nodes
            try:
                node = self.process_statement()
                if node:
                    nodes.append(node)
            except LeaveBlock:
                return nodes
            except UnknownSyntax:
                print self.short
                print self.tokens
                raise 


    def process_statement(self):
        pass

    def process_expression(self, until='EOF'):
        pass


class PythonAnalyzer (Analyzer):
    def process_statement(self):
        print 'Statement>', self._peek().type, self.short[:5], self.tokens[:5]
        if self._match('L'):
            self._consume(1)
            return None
        
        if self._match('Q'):
            self._consume(1)
            raise LeaveBlock()

        if self._match('<'):
            self._consume(1)
            raise LeaveBlock()

        if self._match('i='):
            n = ASTNode('assignment')
            n.parameters['target'] = self._peek().content
            self._consume(2)
            n.children['value'] = [self.process_expression()]
            return n

        if self._match('P'):
            n = ASTNode('print')
            self._consume(1)
            n.children['value'] = [self.process_expression()]
            return n

        if self._match(r'i\('):
            n = self.process_expression()
            return n

        if self._match('i'):
            n = ASTNode('statement')
            self._consume(1)
            return n

        if self._match('I'):
            n = ASTNode('if')
            self._consume(1)
            n.children['condition'] = [self.process_expression(until='colon')]
            self._consume(3)
            n.children['code-true'] = self.process_codeblock()
            if self._match('E'):
                self._consume(3)
                n.children['code-false'] = self.process_codeblock()
            else:
                n.children['code-false'] = []
            return n

        if self._match('FiN'):
            n = ASTNode('for')
            self._consume(1)
            n.parameters['variable'] = self._peek().content
            self._consume(2)
            n.children['source'] = [self.process_expression(until='colon')]
            self._consume(3)
            n.children['code'] = self.process_codeblock()
            return n

        raise UnknownSyntax()

    def process_expression(self, until=['EOL', 'EOF']):
        n = None
        
        print 'Expression>', self._peek().type, self.short[:5], self.tokens[:5]
        while True:
            print self._peek().type, until
            if self._peek().type in until:
                return n

            if self._match('s'):
                n = ASTNode('string')
                n.parameters['value'] = self._peek().content[1:-1]
                self._consume(1)
                continue

            if self._match(r'i\('):
                #print '>>>', self.tokens
                n = ASTNode('call')
                n.parameters['name'] = self._peek().content
                self._consume(2)
                n.children['args'] = []
                while True:
                    a = self.process_expression(until=['brace-close', 'comma'])
                    if a:
                        n.children['args'].append(a)
                    if self._peek().type == 'comma':
                        self._consume(1)
                        continue
                    else:
                        self._consume(1)
                        break
                continue

            if self._match('i'):
                n = ASTNode('variable')
                n.parameters['name'] = self._peek().content
                self._consume(1)
                continue

            if self._match('1'):
                n = ASTNode('integer')
                n.parameters['value'] = int(self._peek().content)
                self._consume(1)
                continue
            
            if self._match('2'):
                n = ASTNode('float')
                n.parameters['value'] = float(self._peek().content)
                self._consume(1)
                continue

            if self._match(r'\('):
                self._consume(1)
                n = self.process_expression(until=['brace-close'])
                self._consume(1)
                continue

            if self._peek().type in ['plus', 'minus', 'multiply', 'divide', 'modulo', 'op-equals', 'op-less']:
                nn = ASTNode(self._peek().type)
                self._consume(1)
                nn.children['left'] = [n]
                n = nn
                n.children['right'] = [self.process_expression(until=until)]
                continue

            return n
