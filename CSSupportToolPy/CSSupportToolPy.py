
from flask import Flask, jsonify, request

class tModel(object):
    """ XXX """
    
    def __init__(self) -> None:
        """ XXX """
        self.sample1
        self.sample2
        self.sample3
        self.sample4
        return


app = Flask(__name__)

# GET���N�G�X�g�ɑ΂��郋�[�g�G���h�|�C���g
@app.route('/api/data', methods=['GET'])
def get_data():
    data = {'message': 'Hello, world!'}
    return jsonify(data)

# POST���N�G�X�g�ɑ΂��郋�[�g�G���h�|�C���g
@app.route('/api/data', methods=['POST'])
def post_data():
    request_data = request.get_json()
    print(request_data)
    # ���N�G�X�g����f�[�^���擾���ď�������
    # ...

    #response_data = {'message': 'Data received successfully'}
    response_data = request_data;
    return jsonify(response_data)

if __name__ == '__main__':
    app.run(port = 5000)