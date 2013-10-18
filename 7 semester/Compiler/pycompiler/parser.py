import re

class Token (object):
    def __init__(self, type, content, short):
        self.type, self.content, self.short = type, content, short

    def __repr__(self):
        return '%s: "%s"' % (self.type, self.content)


class Parser (object):
    def __init__(self):
        self.tokens = []
        for entry in self.setup():
            self.tokens.append((entry[0], (lambda entry: lambda s, t: Token(entry[1], t, entry[2]))(entry)))

    def setup(self):
        pass

    def parse(self, data):
        scanner = re.Scanner(self.tokens)
        tokens = scanner.scan(data)[0]
        tokens = filter(lambda x: x.type is not None, tokens)
        for token in tokens:
            token.content = token.content.strip()
        return tokens


class PythonParser (Parser):
    def setup(self):
        return [
            (r'/', 'divide', '/'),
            (r'\*', 'multiply', '*'),
            (r'-', 'minus', '-'),
            (r'\+', 'plus', '+'),
            (r'%', 'modulo', '%'),
            (r'==', 'op-equals', 'e'),
            (r'<', 'op-less', 'l'),

            (r'\(', 'brace-open', '('),
            (r'\)', 'brace-close', ')'),

            (r',', 'comma', ','),
            (r':', 'colon', ':'),

            (r'print\s+', 'keyword-print', 'P'),
            (r'if\s+', 'keyword-if', 'I'),
            (r'for\s+', 'keyword-for', 'F'),
            (r'in\s+', 'keyword-in', 'N'),
            (r'else:', 'keyword-else-colon', 'E'),
            
            (r'=', 'equals', '='),

            (r'-?\d+\.\d+', 'float', '2'),
            (r'-?\d+', 'integer', '1'),
            (r'[\w_][\w\d_]*', 'identifier', 'i'),
            (r'\"[^"]*\"', 'string', 's'),
            
            (r'#.*', None, None),
            (r'\s+', None, None),
        ]

    def parse(self, data):
        tokens = []
        last_indent = 0
        for l in data.splitlines():
            if l:
                new_indent = 0
                while l.startswith('    '):
                    new_indent += 1
                    l = l[4:]
                for _ in range(new_indent - last_indent):
                    tokens.append(Token('indent', None, '>'))
                for _ in range(last_indent - new_indent):
                    tokens.append(Token('outdent', None, '<'))
                last_indent = new_indent

                tokens += Parser.parse(self, l)
                tokens.append(Token('EOL', None, 'L'))
        tokens.append(Token('EOF', None, 'Q'))
        return tokens
