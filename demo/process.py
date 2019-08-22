import base64

def get_encoded_file(filename):
    with open(filename, "rb") as file:
        encoded_string = base64.b64encode(file.read())
        return encoded_string

def get_encoded_text(text):
    return base64.b64encode(text.encode("utf-8"))

def write_to_file(filename, text):

    with open(filename, "wb") as file:
        file.write(text)



#write_to_file("preprocessed.txt", get_encoded_file("test_doc.pdf"))

write_to_file("safe.txt", get_encoded_text('Rithin Chalumuri'))


